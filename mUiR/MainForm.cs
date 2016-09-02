using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
////using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
//using System.Security.AccessControl;
using System.Diagnostics;

using muir.Model;

namespace muir
{    
    public delegate int NodeComparer(object a, object b);

    public partial class MainForm : Form
    {
        public class NodeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                TreeNode tx = x as TreeNode;
                TreeNode ty = y as TreeNode;

                object a = tx.Tag;
                object b = ty.Tag;

                if (a is Interfejs && b is Interfejs)
                    return (a as Interfejs).CompareTo(b as Interfejs);
                else
                    return 0;
            }
        }

        #region Sekcja inicjalizacji

        private RodzajUmowy m_rodzajUmowy = RodzajUmowy.ODzielo;
        private TypRachunku m_typRachunku = TypRachunku.Jednorazowy;
        private AktywneSzablony szablony = new AktywneSzablony();
        private Konfiguracja konfiguracja = new Konfiguracja();
        //private bool m_islogon = false;
        //identyfikator aplikacji(procesu)
        private static long m_lockid = DateTime.Now.ToBinary();

        public MainForm()
        {
            InitializeComponent();
            //Properties.Resources.SzablonDzielo = "";
            setVisible(false);
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            string plik = Application.StartupPath + @"\mUiR.xml";
            if (File.Exists(plik))
            {
                this.konfiguracja = (Konfiguracja)Loader.Load(typeof(Konfiguracja), plik);
                if (this.konfiguracja == null)
                    this.konfiguracja = new Konfiguracja();
            }

            mainTreeView.TreeViewNodeSorter = new NodeSorter();
        }

        public int nodeCompare(object a, object b)
        {
            if (a is Interfejs && b is Interfejs)
                return (a as Interfejs).CompareTo(b as Interfejs);
            else
                return 0;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Loader.Unload(typeof(Konfiguracja), Application.StartupPath + @"\mUiR.xml", konfiguracja);

        }

        #endregion

        #region Funkcje pomocnicze

        public void nowyKomunikat(string komunikat)
        {
            this.mainToolStripStatusLabel.Text = komunikat;
        }

        public void noweZdarzenie(string msg)
        {
            zdarzeniaListView.Items.Insert(0, 
                DateTime.Now.ToShortTimeString() + ": " + msg);
        }

        public void setVisible(bool visible)
        {
            this.propPanel.Visible = visible;
            this.rachunekPanel.Visible = visible;
            this.umowaPanel.Visible = visible;
            this.osobaPanel.Visible = visible;
            previewToolStripButton.Checked = previewToolStripMenuItem.Checked = this.printPanel.Visible = visible;
            this.previewPanel.Visible = visible;
            imageToolStripButton.Checked = imageToolStripMenuItem.Checked = this.imagePanel.Visible = visible;
            infoToolStripButton.Checked = infoToolStripMenuItem.Checked = this.infoPanel.Visible = visible;
            this.rachunkiPanel.Visible = visible;
        }

        public FileInfo findFileInfo(string path, string ext)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            
            if (info.Exists == false)
                return null;

            FileInfo[] files = null;
            try
            {
                //Thread.Sleep(30);
                files = info.GetFiles("*." + ext, SearchOption.TopDirectoryOnly);
            }
            catch (IOException)
            {
                return null;
            }
            
            if (files.Length > 0)
                return files[0];
            else
                return null;
        }

        public void aktualizujOstatnieProjekty(string plik)
        {
            if (!string.IsNullOrEmpty(plik))
            {
                if (konfiguracja.OstatnieProjekty.Contains(plik))
                    konfiguracja.OstatnieProjekty.Remove(plik);

                konfiguracja.OstatnieProjekty.Insert(0, plik);
            }

            bool rem = false;
            do
            {
                rem = false;
                foreach (string p in konfiguracja.OstatnieProjekty)
                {
                    if (Directory.Exists(p) == false)
                    {
                        konfiguracja.OstatnieProjekty.Remove(p);
                        rem = true;
                        break;
                    }
                }
            } while (rem);


            if (konfiguracja.OstatnieProjekty.Count > 10)
            {
                konfiguracja.OstatnieProjekty.RemoveAt(konfiguracja.OstatnieProjekty.Count - 1);
            }
            /*
            ostatnieProjektyToolStripMenuItem.DropDownItems.Clear();
            foreach (string p in konfiguracja.OstatnieProjekty)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(p);
                item.Click += new EventHandler(item_Click);
                this.ostatnieProjektyToolStripMenuItem.DropDownItems.Add(item);
            }
            */
        }

        #endregion

        #region Główne centrum zarządzania na obiektach

        public int checkOsoba(TreeNode osobaNode)
        {
            Color color = Color.Black;
            foreach (TreeNode node in osobaNode.Nodes)
            {
                if (checkUmowa(node) > 0.0m)
                    color = Color.Red;
            }
            //osobaNode.ForeColor = color;
            if (color != Color.Black)
                setColor(osobaNode, color);
            //new Font(osobaNode.NodeFont, FontStyle.Bold);
            //osobaNode.NodeFont.OriginalFontName;
            return 0;
        }

        public void setColor(TreeNode node, Color color)
        {
            node.ForeColor = color;
        }

        public decimal checkUmowa(TreeNode umowaNode)
        {
            
            Umowa umowa = umowaNode.Tag as Umowa;
            decimal suma = 0.0m;

            if (umowaNode.Nodes.Count == 0)
            {
                umowaNode.ForeColor = Color.Black;
                nowyKomunikat("dla wybranej umowy nie wystawiono żadnych rachunków");
                return suma - umowa.Kwota;
            }

            foreach (TreeNode node in umowaNode.Nodes)
            {
                Rachunek rachunek = node.Tag as Rachunek;
                suma += rachunek.Wynagrodzenie;
                if (suma > umowa.Kwota)
                {
                    //oznacz dany wezel na czerwono ze przekracza wartosc kwoty umowy
                    //node.ForeColor = Color.Red;
                    setColor(node, Color.Red);
                }
                else
                    setColor(node, Color.Black);
            }

            if (suma > umowa.Kwota)
            {
                //oznacz umowe na czerwono
                //umowaNode.ForeColor = Color.Red;
                setColor(umowaNode, Color.Red);
                nowyKomunikat("UWAGA: dla wybranej umowy przekroczono kwotę umowy " + umowa.Kwota + " o " + (suma - umowa.Kwota).ToString());
            }
            else
            {
                if (Math.Abs(suma - umowa.Kwota) < 0.01m)
                {
                    //umowaNode.ForeColor = Color.Green;
                    setColor(umowaNode, Color.Gray);
                    nowyKomunikat("dla wybranej umowy wystawiono rachunki na pełną kwotę " + umowa.Kwota);
                }
                else
                {
                    nowyKomunikat(@"dla wybranej umowy pozostało jeszcze " + (umowa.Kwota - suma) + " na rachunki z " + umowa.Kwota);
                    //umowaNode.ForeColor = Color.Black;
                    setColor(umowaNode, Color.Green);
                }
            }
            return suma - umowa.Kwota;
        }

        private int m_lastIndeks = 0;

        public void manageObiekt_OnClick(TreeNode SelectedNode)
        {

            setVisible(false);

            object obiekt = SelectedNode.Tag;

            if (obiekt == null)
                return ;

            if (SelectedNode == null)
                return ;

            if (obiekt is Interfejs)
            {
                infoToolStripButton.Checked = infoToolStripMenuItem.Checked = this.infoPanel.Visible = true;
                this.infoTextLabel.Text = "Typ: " + (obiekt as Interfejs).Typ + "\n" +
                    (obiekt as Interfejs).get_Info();
            }
            else
                this.infoTextLabel.Text = "";

            if (obiekt is Projekt)
            {
                this.rachunkiPanel.Visible = true;
                this.rachunkiLabel.Text = "Aktywne rachunki osób dla " + SystemInformation.UserName;

                this.osobaPanel.Visible = true;
                this.infoTextLabel.Text = (obiekt as Projekt).get_Info();
                TreeNode projNode = SelectedNode;
                rachunkiListView.Items.Clear();
                Projekt projekt = obiekt as Projekt;
                foreach (TreeNode node in projNode.Nodes)
                {
                    checkOsoba(node);
                    Osoba osoba = node.Tag as Osoba;
                    foreach (TreeNode uNode in node.Nodes)
                    {
                        Umowa umowa = uNode.Tag as Umowa;
                        foreach (TreeNode rNode in uNode.Nodes)
                        {
                            Rachunek rachunek = rNode.Tag as Rachunek;
                            if (rachunek.Aktywny == TakNie.Tak && 
                                (rachunek.Uzytkownik.ToLower() == SystemInformation.UserName.ToLower() || 
                                projekt.Uzytkownik.ToLower() == SystemInformation.UserName.ToLower()))
                            {
                                ListViewItem item = new ListViewItem(osoba.Imie + " " + osoba.Nazwisko);
                                item.SubItems.Add(umowa.Numer + " (" + uNode.Nodes.Count + ")");
                                item.SubItems.Add(RodzajUmowyValue.get_Value(umowa.Rodzaj));
                                item.SubItems.Add(umowa.DataOd.ToShortDateString() + " " + umowa.DataDo.ToShortDateString());
                                item.SubItems.Add(TypRachunkuValue.get_Value(rachunek.Rodzaj));
                                item.SubItems.Add(rachunek.Wynagrodzenie.ToString());
                                item.SubItems.Add(rachunek.Robota);
                                item.SubItems.Add(rachunek.Uzytkownik);
                                item.Tag = rNode;

                                rachunkiListView.Items.Add(item);

                                if (item.Index == m_lastIndeks)
                                {
                                    item.Selected = true;
                                    item.EnsureVisible();
                                }
                            }
                        }
                    }
                }
            }
            else if (obiekt is Osoba)
            {
                //this.infoTextLabel.Text = "Osoba";
                this.umowaPanel.Visible = true;
                
                FileInfo jpg = findFileInfo(SelectedNode.FullPath, "jpg");
                if (jpg != null)
                {
                    infoTextLabel.Text += "\n" + "Zdjęcie: " + jpg.Name;
                    this.imagePictureBox.Image = Image.FromFile(jpg.FullName);
                    imageToolStripButton.Checked = imageToolStripMenuItem.Checked = this.imagePanel.Visible = true;
                }
                else
                {
                    infoTextLabel.Text += "\n" + "Zdjęcie: brak";
                    this.imagePictureBox.Image = this.treeImageList.Images["Osoba"];
                    imageToolStripButton.Checked = imageToolStripMenuItem.Checked = this.imagePanel.Visible = false;
                }
                this.imagePictureBox.Refresh();
                //po wybraniu osoby sprawdz poprawnosc umow
                TreeNode osobaNode = SelectedNode;
                checkOsoba(osobaNode);
            }
            else if (obiekt is Umowa)
            {
                previewToolStripButton.Checked = previewToolStripMenuItem.Checked = this.printPanel.Visible = true;
                this.previewPanel.Visible = true;
                //po wybraniu obiektu umowy bedzie przeprowadzane sprawdzenie czy 
                //suma wynagrodzen rachunku nie zostala przekroczona
                AktywneSzablony.AktywnaUmowa = obiekt as Umowa;
                //this.infoTextLabel.Text = "Umowa";
                this.rachunekPanel.Visible = true;
                TreeNode umowaNode = SelectedNode;
                decimal roznica = checkUmowa(umowaNode);
                infoTextLabel.Text += "\n" + "Suma rachunków: " + ((obiekt as Umowa).Kwota + roznica) + " zł";
                if (roznica > 0.0m)
                    infoTextLabel.Text += " (+" + roznica + " zł)";
                else
                    infoTextLabel.Text += " (" + roznica + " zł)";

                string doc = SelectedNode.FullPath + @"\_umowa.rtf";
                if (File.Exists(doc))
                    infoTextLabel.Text += "\n" + "Plik: _umowa.rtf";
                else
                    infoTextLabel.Text += "\n" + "Plik: (brak)";

                TreeNode n = SelectedNode;
                Umowa umowa = n.Tag as Umowa;
                Osoba osoba = n.Parent.Tag as Osoba;

                rtfPrintDocument.DocumentName = umowa.Nazwa;
                string rtf = Loader.GenUmowa(osoba, umowa);
                printRichTextBoxPrintCtrl.Rtf = rtf;
                printRichTextBoxPrintCtrl.Tag = rtf;
            }
            else if (obiekt is Rachunek)
            {
                string rtf = SelectedNode.FullPath + @"\_rachunek.rtf";
                if (File.Exists(rtf))
                    infoTextLabel.Text += "\n" + "Plik: _rachunek.rtf";
                else
                    infoTextLabel.Text += "\n" + "Plik: (brak)";
                //setVisible(false);
                //this.infoTextLabel.Text = "Rachunek";
                previewToolStripButton.Checked = previewToolStripMenuItem.Checked = this.printPanel.Visible = true;
                this.previewPanel.Visible = true;

                Rachunek rachunek = obiekt as Rachunek;
                decimal w = rachunek.Wynagrodzenie;
                TreeNode n = SelectedNode;

                Umowa umowa = n.Parent.Tag as Umowa;
                Osoba osoba = n.Parent.Parent.Tag as Osoba;

                rtfPrintDocument.DocumentName = rachunek.Nazwa;
                //rachunekRichTextBoxPrintCtrl.Text = szablony.get_DoWyplaty(umowa.Rodzaj, rachunek.Wynagrodzenie).ToString();
                rtf = Loader.GenRtf(osoba, umowa, rachunek);
                printRichTextBoxPrintCtrl.Rtf = rtf;
                printRichTextBoxPrintCtrl.Tag = rtf;
                //printRichTextBoxPrintCtrl.Text = rtf;
            }
            else
                return ;

            return ;
        }

        #endregion

        #region Zdarzenia dla głównego drzewa mainTreeView

        private void mainTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            mainTreeView.SelectedNode = e.Node;
            propPropertyGrid.SelectedObject = e.Node.Tag;
            propPanel.Height = 200;
        }

        private void mainTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            manageObiekt_OnClick(e.Node);
        }

        private void mainTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Label) || e.Label == e.Node.Name)
            {
                nowyKomunikat("zmiana nazwy: anulowano");
                e.CancelEdit = true;
                return;
            }
            else
            {
                char[] chars = Path.GetInvalidFileNameChars();
                foreach (char c in chars)
                    if (e.Label.Contains(c + ""))
                    {
                        e.CancelEdit = true;
                        nowyKomunikat("zmiana nazwy: nazwa zawiera niedozwolone znaki");
                        return;
                    }
            }

            string newName = e.Label;
            string oldName = e.Node.Name;

            //e.Node.Expand();

            string rootPath = mainFileSystemWatcher.Path;

            //string oldPath = rootPath + Path.DirectorySeparatorChar + e.Node.FullPath;
            string oldPath = e.Node.FullPath;
            //jezeli glowny wezel za nazwe ma rootPath to wystarczy uzyc tylko nazwy wezla
            string newPath = rootPath + Path.DirectorySeparatorChar + newName;
            if (e.Node.Parent != null)
                newPath = e.Node.Parent.FullPath + Path.DirectorySeparatorChar + newName;

            //zmiana nazwy katalogu wymusi zdarzenie zmiany nazwy katalogu, które przechwyci FileSystemWatcher
            //i już sam zajmie się aktualizacją nowego węzła w drzewie
            //ja muszę się tylko upewnić że nowy katalog jeszcze nie istnieje, a stary tak

            if (Directory.Exists(oldPath) && !Directory.Exists(newPath))
            {
                try
                {
                    string fileLock = e.Node.Parent.FullPath + @"\" + m_lockid + ".lock";

                    FileStream stream = null;
                    try
                    {
                        stream = File.Create(fileLock, 1024, FileOptions.DeleteOnClose);
                    }
                    catch (IOException)
                    {

                    }

                    Directory.Move(oldPath, newPath);
                    nowyKomunikat("zmiana nazwy: " + oldPath + "->" + newPath + " (OK)");
                    stream.Close();
                }
                catch (IOException ex)
                {
                    nowyKomunikat("zmiana nazwy: " + oldPath + "->" + newPath + " (BŁĄD - " + ex.Message + ")");
                    e.CancelEdit = true;
                }
            }
            else
            {
                nowyKomunikat("zmiana nazwy: " + oldPath + "->" + newPath + " (BŁĄD)");
                e.CancelEdit = true;
            }

            //nie ma koniecznosci jawnego wywolania funkcji renameNode
            //renameNode("rootPath", "newPath", "oldPath");
        }

        private void mainTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            
            if (e.Node.Level == 0)
            {
                nowyKomunikat("zmiana nazwy: zabronione");
                e.CancelEdit = true;
                return;
            }
            nowyKomunikat("zmiana nazwy: " + e.Node.Name);
        }

        private void mainTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            showConfig(e.Node);
        }

        #endregion

        #region Zdarzenia dla głównego menu mainMenu

        private void polaczItem_Click(object sender, EventArgs e)
        {
            //noweZdarzenie("Połącz z...");
            aktualizujOstatnieProjekty(mainFolderBrowserDialog.SelectedPath);

            if (konfiguracja.OstatnieProjekty.Count > 0)
                mainFolderBrowserDialog.SelectedPath = konfiguracja.OstatnieProjekty[0];

            DialogResult result = mainFolderBrowserDialog.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            if (mainTreeView.TopNode != null)
            {
                closeConnection();
            }

            aktualizujOstatnieProjekty(mainFolderBrowserDialog.SelectedPath);

            FileInfo file = findFileInfo(mainFolderBrowserDialog.SelectedPath, "xml");
            if (file == null)
            {
                nowyKomunikat("UWAGA: lokacja nie została zainicjowana");
                noweZdarzenie("UWAGA: lokacja nie została zainicjowana");
                result = MessageBox.Show(this,
                    "UWAGA: lokacja nie została zainicjowana, jesteś pewien że chcesz się podłączyć?\n" +
                    "Jeżeli jesteś pewien co robisz wybierz - Tak, a jeżeli nie to wybierz - Nie",
                    "Połącz z...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    noweZdarzenie("Połącz z " + mainFolderBrowserDialog.SelectedPath + " (Anulowano)");
                    return;
                }
            }
            else
            {
                nowyKomunikat("lokacja jest już zainicjowana");
                object projekt = Loader.Load(typeof(Projekt), file.FullName);
                if (projekt == null)
                {
                    string msg = "UWAGA: lokacja wydaje się być zainicjowana, ale plik konfiguracyjny jest uszkodzony lub nieprawidłowy";
                    nowyKomunikat(msg);
                    noweZdarzenie(msg);
                    MessageBox.Show(this, msg, "Połącz z...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    noweZdarzenie("Połącz z " + mainFolderBrowserDialog.SelectedPath + " (Błąd)");
                    return;
                }
            }

            mainFileSystemWatcher.Path = mainFolderBrowserDialog.SelectedPath;
            mainFileSystemWatcher.EnableRaisingEvents = true;
            buildTree(mainFileSystemWatcher.Path);
            mainTreeView.TopNode.Expand();
            noweZdarzenie("Połącz z " + mainFileSystemWatcher.Path + " (OK)");
        }

        private void rozlaczToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeConnection();
        }

        public void closeConnection()
        {
            if (mainTreeView.TopNode == null)
                return;

            nowyKomunikat("Rozłączenie z " + mainTreeView.TopNode.Name + " (OK)");
            noweZdarzenie("Rozłączenie z " + mainTreeView.TopNode.Name + " (OK)");
            
            propPropertyGrid.SelectedObject = null;
            propPanel.Height = 100;

            mainFileSystemWatcher.EnableRaisingEvents = false;
            mainTreeView.Nodes.Clear();

            setVisible(false);
        }

        private void zakonczToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Zdarzenia dla FileSystemWatcher

        private void mainFileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
//            DateTime t1 = DateTime.Now;
            string path = Path.GetDirectoryName(e.FullPath);
            Thread.Sleep(100);

            string fileLock = path + @"\" + m_lockid + ".lock";
            FileInfo isLock = findFileInfo(path, "lock");

            if (isLock != null)
            {
                string id = Path.GetFileNameWithoutExtension(isLock.Name);
                
                if (id != m_lockid.ToString())
                {
                    noweZdarzenie("ktoś zmienia nazwę obiektu " + e.Name + ", czekaj...");
                    //ktos inny zalozyl blokade
                    int time = 0;
                    do
                    {
                        time += 500;
                        Thread.Sleep(500);
                        //czekaj az plik zostanie usuniety
                        if (time > 5 * 500) //dluzej juz nie moge
                            break;
                    } while (File.Exists(isLock.FullName));
                }
            }

            renameNode(mainFileSystemWatcher.Path, e.FullPath, e.OldFullPath);

            //DateTime t2 = DateTime.Now;
        }

        private void mainFileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            DateTime t1 = DateTime.Now;
            Thread.Sleep(100);
            //zostal utworzony nowy katalog
            //w katalogu powinien byc plik insert.lockid
            //jezeli lockid jest takie jak id aplikacji
            //to znaczy ze my go utworzylismy
            //jezeli id jest inne to powinnismy poczekac az plik zostanie usuniety
            //jezeli brak pliku to znaczy ze obiekt zostal dodany spoza aplikacji
            string path = Path.GetDirectoryName(e.FullPath);
            string fileLock = path + @"\" + m_lockid + ".lock";
            FileInfo isLock = findFileInfo(path, "lock");

            if (isLock != null)
            {
                string id = Path.GetFileNameWithoutExtension(isLock.Name);
                
                if (id != m_lockid.ToString())
                {
                    noweZdarzenie("ktoś tworzy obiekt " + e.Name + ", czekaj...");
                    //ktos inny zalozyl blokade
                    int time = 0;
                    do
                    {
                        time += 500;
                        Thread.Sleep(500);
                        //czekaj az plik zostanie usuniety
                        if (time > 5 * 500) //dluzej juz nie moge
                            break;
                    } while (File.Exists(isLock.FullName));
                }
            }

            createNode(mainFileSystemWatcher.Path, e.FullPath);

            DirectoryInfo info = new DirectoryInfo(e.FullPath);

            DateTime t2 = DateTime.Now;
            string msg = string.Format("nowy obiekt: {1} ({0}) {2:F0}ms",
                info.CreationTime, e.FullPath,
                (t2 - t1).Milliseconds);
            noweZdarzenie(msg);
        }

        private void mainFileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            deleteNode(mainFileSystemWatcher.Path, e.FullPath);
        }

        #endregion

        #region Funkcje dla mainTreeView

        private bool m_build = false;
        //zbuduj drzewo od nowa
        public void buildTree(string rootPath)
        {
            //mainTreeView.Nodes.Clear();
            nowyKomunikat("wczytywanie: " + rootPath + "...");
            m_build = true;
            if (mainTreeView.TopNode == null)
            {
                TreeNode node = mainTreeView.Nodes.Add(rootPath, rootPath, "Projekt", "Projekt");
                customizeNode(rootPath, node);
            }
            
            DirectoryInfo diProjekt = new DirectoryInfo(rootPath);
            
            DirectoryInfo[] diOsoby = diProjekt.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo diOsoba in diOsoby)
            {
                //if(mainTreeView.Nodes.ContainsKey(diOsoba.FullName))
                //    continue;
                createNode(rootPath, diOsoba.FullName);
                Application.DoEvents();
                DirectoryInfo[] diUmowy = diOsoba.GetDirectories("*", SearchOption.TopDirectoryOnly);
                foreach (DirectoryInfo diUmowa in diUmowy)
                {
                    //if (mainTreeView.Nodes.ContainsKey(diUmowa.FullName))
                    //    continue;
                    createNode(rootPath, diUmowa.FullName);
                    Application.DoEvents();
                    DirectoryInfo[] diRachunki = diUmowa.GetDirectories("*", SearchOption.TopDirectoryOnly);
                    foreach (DirectoryInfo diRachunek in diRachunki)
                    {
                        //if (mainTreeView.Nodes.ContainsKey(diRachunek.FullName))
                        //    continue;
                        createNode(rootPath, diRachunek.FullName);
                        Application.DoEvents();
                    }
                }
            }
            nowyKomunikat("wczytywanie: " + rootPath + " (OK)");
            
            mainTreeView.Sort();
            m_build = false;
        }

        public void refreshNode()
        {
            TreeNode node = mainTreeView.SelectedNode;
            DirectoryInfo diNode = new DirectoryInfo(node.FullPath);
            
            DirectoryInfo[] diOsoby = diNode.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo diOsoba in diOsoby)
            {
                if (node.Nodes.ContainsKey(diOsoba.FullName))
                    continue;
                createNode(mainFileSystemWatcher.Path, diOsoba.FullName);
                Application.DoEvents();
            }
        }

        //aktualizuj istniejace drzewo 
        public void updateTree(string rootPath)
        {
        }

        //build tree od nowa
        public void rebuildTree(string rootPath)
        {
            mainTreeView.Nodes.Clear();
            buildTree(rootPath);
        }

        public void createNode(string rootPath, string path)
        {
            //c:\\a\b\c c:\\a\b\c\d -> d
            string relativePath = path.Replace(rootPath, "");
            
            if (mainTreeView.Nodes.Count == 0)
            {
                mainTreeView.Nodes.Add(rootPath, rootPath, "Projekt", "Projekt");
            }

            //szukaj wezla dla nowego obiektu
            string[] dirs = relativePath.Split(new string[] { "\\" }, 
                StringSplitOptions.RemoveEmptyEntries);
            //TreeNode node = mainTreeView.TopNode;
            TreeNode node = mainTreeView.Nodes[0];

            customizeNode(rootPath, node);

            if (dirs.Length > 3)
                return;

            foreach (string dir in dirs)
            {
                if (string.IsNullOrEmpty(dir))
                    continue;
                if (node.Nodes.ContainsKey(dir) == false)
                {
                    node = node.Nodes.Add(dir, dir);
                }
                else
                    node = node.Nodes[dir];
            }

            customizeNode(path, node);

            DirectoryInfo info = new DirectoryInfo(path);
            string msg = string.Format("nowy obiekt: {1} ({0})",
                info.CreationTime, node.FullPath);
            nowyKomunikat(msg);
            //noweZdarzenie(msg);
            if (m_build == false)
                mainTreeView.Sort();
        }

        /// <summary>
        /// gdy wezel jest tworzony doczepiany jest do niego obiekt
        /// oraz tworzone jest do danego obiektu menu
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        public void customizeNode(string path, TreeNode node)
        {
            //noweZdarzenie("custom node:" + path + ", node " + node.FullPath);
            ContextMenuStrip menu = new ContextMenuStrip();
            node.ContextMenuStrip = menu;
            
            ToolStripMenuItem dodajNode = new ToolStripMenuItem();
            //dodajNode.Click += new EventHandler(dodajNode_Click);
            
            ToolStripMenuItem usunNode = new ToolStripMenuItem("Usuń");
            usunNode.Click += new EventHandler(usunNode_Click);
            
            ToolStripMenuItem configNode = new ToolStripMenuItem();
            configNode.Click += new EventHandler(configNode_Click);
            configNode.Text = "Właściwości";
            
            ToolStripItem renameNode = new ToolStripMenuItem("Zmień nazwę");
            renameNode.Click += new EventHandler(renameNode_Click);

            ToolStripMenuItem refreshNode = new ToolStripMenuItem("Odśwież");
            refreshNode.Click += new EventHandler(refreshNode_Click);

            FileInfo jpgFile = findFileInfo(path, "jpg");
            FileInfo xmlFile = findFileInfo(path, "xml");
            //FileInfo rtfFile = findFileInfo(path, "rtf");
            //FileInfo docFile = findFileInfo(path, "doc");

            object obiekt = null;
            //na podstawie glebokosci wezla decyduje jaki typ obiektu
            //powinien zostac utworzony
            //moze byc wiele roznych drzew z roznymi schematami i poziomami

            switch (node.Level)
            {
                case 0:
                    {
                        //inicjacja konfiguracji
                        bool update = false;
                        Projekt projekt = null;
                        if (xmlFile != null)
                        {
                            projekt = (Projekt)Loader.Load(typeof(Projekt), xmlFile.FullName);
                            if (projekt.Nazwa != Path.GetFileName(node.Name) ||
                                string.IsNullOrEmpty(projekt.Uzytkownik))
                                update = true;
                        }
                        else
                        {
                            update = true;
                            projekt = new Projekt(Path.GetFileName(node.Name));
                            projekt.Uzytkownik = SystemInformation.UserName;
                        }

                        if (update)
                        {
                            projekt.DataAktualizacji = DateTime.Now;
                            projekt.Nazwa = Path.GetFileName(node.Name);
                            projekt.Opis = "";
                            
                            //nie udalo sie zapisac, najwyrazniej w tym samym momencie ktos inny to robil
                            //powinnismy zaktualizowac ten wezel pozniej albo tez go oznaczyc ze jest 
                            //jakas bardziej aktualna wersja
                            bool ok = Loader.Unload(typeof(Projekt), node.Name + @"\_node.xml", projekt);
                            if (ok == false)
                                noweZdarzenie("zapis obiektu " + node.Name + " nie powiódł się (najwyrazniej ktoś inny w tym samym momencie modyfikował obiekt)");
                        }

                        obiekt = projekt;

                        //wybranie ikon
                        node.ImageKey = "Projekt";
                        node.SelectedImageKey = "Projekt";
                        node.ToolTipText = projekt.get_Info();

                        //zbudowanie menu
                        dodajNode.Click += new EventHandler(dodajNode_Click);
                        dodajNode.Text = "Dodaj osobę";
                        menu.Items.Add(dodajNode);
                        menu.Items.Add(refreshNode);
                        menu.Items.Add(new ToolStripSeparator());
                        menu.Items.Add(configNode);
                    }
                    break;
                case 1:
                    {
                        //inicjacja konfiguracji
                        Osoba osoba = null;
                        bool update = false;
                        if (xmlFile != null)
                        {
                            osoba = (Osoba)Loader.Load(typeof(Osoba), xmlFile.FullName);
                            if (osoba.Nazwa != node.Name || string.IsNullOrEmpty(osoba.Uzytkownik))
                                update = true;
                        }
                        else
                        {
                            osoba = new Osoba(node.Name);
                            osoba.Opis = "";
                            osoba.Uzytkownik = SystemInformation.UserName;
                            update = true;
                        }

                        if (update)
                        {
                            osoba.DataAktualizacji = DateTime.Now;
                            osoba.Nazwa = node.Name;
                            string[] imieinazwisko = node.Name.Split(
                                new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            osoba.Imie = "";
                            osoba.Nazwisko = imieinazwisko[imieinazwisko.Length - 1];
                            for (int i = 0; i < imieinazwisko.Length - 1; i++)
                                osoba.Imie += imieinazwisko[i] + " ";
                            osoba.Imie = osoba.Imie.Trim();
                            
                            bool ok = Loader.Unload(typeof(Osoba),
                                path + Path.DirectorySeparatorChar + @"\_node.xml", osoba);
                            if (ok == false)
                                noweZdarzenie("zapis obiektu " + node.Name + " nie powiódł się (najwyrazniej ktoś inny w tym samym momencie modyfikował obiekt)");
                        }

                        obiekt = osoba;
                        //wybranie ikon
                        node.ImageKey = "Osoba";
                        node.SelectedImageKey = "Osoba";
                        node.ToolTipText = osoba.get_Info();
                        
                        //zbudowanie menu
                        dodajNode.Text = "Dodaj umowę";

                        ToolStripMenuItem subnode1 =
                            new ToolStripMenuItem(RodzajUmowyValue.get_Value(RodzajUmowy.ODzielo));
                        ToolStripMenuItem subnode2 =
                            new ToolStripMenuItem(RodzajUmowyValue.get_Value(RodzajUmowy.NaZlecenie));
                        ToolStripMenuItem subnode3 =
                            new ToolStripMenuItem(RodzajUmowyValue.get_Value(RodzajUmowy.NaZlecenieStudent));

                        subnode1.Click += new EventHandler(dodajNode_Click);
                        subnode2.Click += new EventHandler(dodajNode_Click);
                        subnode3.Click += new EventHandler(dodajNode_Click);

                        dodajNode.DropDownItems.Add(subnode1);
                        dodajNode.DropDownItems.Add(subnode2);
                        dodajNode.DropDownItems.Add(subnode3);

                        menu.Items.Add(dodajNode);
                        ToolStripMenuItem dodajZdjecie = new ToolStripMenuItem("Dodaj zdjęcie...");
                        dodajZdjecie.Click += new EventHandler(dodajZdjecie_Click);
                        menu.Items.Add(dodajZdjecie);
                        menu.Items.Add(new ToolStripSeparator());
                        menu.Items.Add(usunNode);
                        menu.Items.Add(renameNode);
                        menu.Items.Add(refreshNode);
                        menu.Items.Add(new ToolStripSeparator());
                        menu.Items.Add(configNode);
                    }
                    break;
                case 2:
                    {
                        bool update = false;
                        Umowa umowa = null;
                        //inicjacja konfiguracji
                        if (xmlFile != null)
                        {
                            umowa = (Umowa)Loader.Load(typeof(Umowa), xmlFile.FullName);
                            this.m_rodzajUmowy = umowa.Rodzaj;
                            this.rodzajUmowyToolStripSplitButton.Text = RodzajUmowyValue.get_Value(this.m_rodzajUmowy);
                            if (umowa.Nazwa != node.Name || string.IsNullOrEmpty(umowa.Uzytkownik) ||
                                string.IsNullOrEmpty(umowa.Numer))
                                update = true;
                        }
                        else
                        {
                            umowa = new Umowa(node.Name);
                            umowa.Uzytkownik = SystemInformation.UserName;
                            umowa.Numer = "";
                            umowa.Opis = "";
                            umowa.Rodzaj = this.m_rodzajUmowy;
                            update = true;
                        }

                        if (update)
                        {
                            umowa.Nazwa = node.Name;
                            //RodzajeUmow.get_SzablonyRachunkow(AktywneSzablony.AktywnaUmowa.Rodzaj)[0];
                            umowa.NazwaSzablonu = RodzajeUmow.get_SzablonyUmow(this.m_rodzajUmowy)[0];

                            if(string.IsNullOrEmpty(umowa.Numer))
                                umowa.Numer = node.Name;
                            umowa.DataAktualizacji = DateTime.Now;
                            
                            bool ok = Loader.Unload(typeof(Umowa),
                                path + Path.DirectorySeparatorChar + @"\_node.xml", umowa);
                            if (ok == false)
                                noweZdarzenie("zapis obiektu " + node.Name + " nie powiódł się (najwyrazniej ktoś inny w tym samym momencie modyfikował obiekt)");
                        }
                        AktywneSzablony.AktywnaUmowa = umowa;

                        obiekt = umowa;

                        //wybranie ikon
                        node.ImageKey = RodzajUmowyValue.get_Value(this.m_rodzajUmowy);
                        node.SelectedImageKey = node.ImageKey;
                        node.ToolTipText = umowa.get_Info();
                            
                        //budowa menu
                        ToolStripMenuItem otworzDoc = new ToolStripMenuItem("Otwórz");
                        otworzDoc.Click += new EventHandler(otworz_Click);
                        menu.Items.Add(otworzDoc);
                        //menu.Items.Add("Otwórz");
                        menu.Items.Add(new ToolStripSeparator());
                        dodajNode.Text = "Dodaj rachunek";
                        menu.Items.Add(dodajNode);

                        ToolStripMenuItem dodajDokument = new ToolStripMenuItem("Dodaj dokument...");
                        dodajDokument.Click += new EventHandler(dodajDokument_Click);
                        menu.Items.Add(dodajDokument);

                        ToolStripMenuItem subnode4 =
                            new ToolStripMenuItem(TypRachunkuValue.get_Value(TypRachunku.Jednorazowy));
                        ToolStripMenuItem subnode5 =
                            new ToolStripMenuItem(TypRachunkuValue.get_Value(TypRachunku.Częściowy));
                        ToolStripMenuItem subnode6 =
                            new ToolStripMenuItem(TypRachunkuValue.get_Value(TypRachunku.Ostateczny));

                        subnode4.Click += new EventHandler(dodajNode_Click);
                        subnode5.Click += new EventHandler(dodajNode_Click);
                        subnode6.Click += new EventHandler(dodajNode_Click);

                        dodajNode.DropDownItems.Add(subnode4);
                        dodajNode.DropDownItems.Add(subnode5);
                        dodajNode.DropDownItems.Add(subnode6);

                        usunNode.Text = "Usuń";
                        menu.Items.Add(new ToolStripSeparator());
                        menu.Items.Add(usunNode);
                        menu.Items.Add(renameNode);
                        menu.Items.Add(refreshNode);
                        menu.Items.Add(new ToolStripSeparator());
                        menu.Items.Add(configNode);
                    }
                    break;
                case 3:
                    {
                        bool update = false;
                        Rachunek rachunek = null;
                        //inicjacja konfiguracji
                        if (xmlFile != null)
                        {
                            //noweZdarzenie("rachunek: " + xmlFile.FullName);
                            //uwaga: po zmianie nazwy typu rachunku
                            //nie można odczytac obiektu rachunek
                            rachunek = (Rachunek)Loader.Load(typeof(Rachunek), xmlFile.FullName);
                            this.m_typRachunku = rachunek.Rodzaj;
                            
                            this.rodzajRachunkuToolStripSplitButton.Text = TypRachunkuValue.get_Value(this.m_typRachunku);
                            if (rachunek.Nazwa != node.Name || string.IsNullOrEmpty(rachunek.Uzytkownik))
                                update = true;
                        }
                        else
                        {
                            rachunek = new Rachunek(node.Name);
                            rachunek.Opis = "";
                            rachunek.Uzytkownik = SystemInformation.UserName;

                            Umowa umowa = AktywneSzablony.AktywnaUmowa;
                            rachunek.NazwaSzablonu = RodzajeUmow.get_SzablonyRachunkow(AktywneSzablony.AktywnaUmowa.Rodzaj)[0];

                            Umowa u = node.Parent.Tag as Umowa;

                            rachunek.WKasie = AktywneSzablony.AktywnaUmowa.RodzajPlatnosci != "przelewem";
                            rachunek.Wynagrodzenie = 0;

                            node.Tag = rachunek;
                            decimal ile = -checkUmowa(node.Parent);

                            if (ile < 0)
                                ile = 0;

                            if (umowa.DataOd.AddMonths(1) >= umowa.DataDo)
                            {
                                rachunek.Wynagrodzenie = ile;
                                rachunek.Rodzaj = TypRachunku.Jednorazowy;
                            }
                            else
                            {
                                rachunek.Rodzaj = TypRachunku.Częściowy;                               
                                rachunek.Wynagrodzenie = ile;
                            }
                            
                            if(m_fixtyp)
                                rachunek.Rodzaj = this.m_typRachunku;

                            rachunek.Robota = umowa.Robota;

                            update = true;
                        }

                        if (update)
                        {
                            rachunek.DataAktualizacji = DateTime.Now;
                            rachunek.Nazwa = node.Name;

                            bool ok = Loader.Unload(typeof(Rachunek),
                                path + Path.DirectorySeparatorChar + @"\_node.xml", rachunek);
                            if (ok == false)
                                noweZdarzenie("zapis obiektu " + node.Name + " nie powiódł się (najwyrazniej ktoś inny w tym samym momencie modyfikował obiekt)");
                        }
                        obiekt = rachunek;

                        //wybranie ikon
                        //node.ImageKey = "Rachunek";
                        node.ImageKey = TypRachunkuValue.get_Value(this.m_typRachunku);
                        node.SelectedImageKey = node.ImageKey;
                        node.ToolTipText = rachunek.get_Info();

                        //budowa menu
                        ToolStripMenuItem otworz = new ToolStripMenuItem("Otwórz");
                        otworz.Click += new EventHandler(otworz_Click);
                        menu.Items.Add(otworz);

                        //menu.Items.Add(new ToolStripSeparator());
                        //menu.Items.Add("Wyrzuć z projektu");
                        menu.Items.Add(new ToolStripSeparator());
                        usunNode.Text = "Usuń";
                        menu.Items.Add(usunNode);
                        menu.Items.Add(renameNode);
                        menu.Items.Add(refreshNode);
                        menu.Items.Add(new ToolStripSeparator());
                        menu.Items.Add(configNode);
                    }
                    break;
                default:
                    node.ImageKey = "Projekt";
                    obiekt = null;
                    break;
            }

            //aktualizacja ikon
            if (jpgFile != null)
            {
                treeImageList.Images.Add(jpgFile.FullName, new Bitmap(jpgFile.FullName));
                node.ImageKey = jpgFile.FullName;
                node.SelectedImageKey = jpgFile.FullName;
            }

            node.Tag = obiekt;
        }

        void dodajZdjecie_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Wybierz zdjęcie, które chcesz dołączyć";
            open.Filter = "Plik JPG (*.jpg)|*.jpg";

            FileInfo file = findFileInfo(mainTreeView.SelectedNode.FullPath, "jpg");
            string jpg = mainTreeView.SelectedNode.FullPath + @"\_zdjecie.jpg";
            if (File.Exists(jpg) || file != null)
            {
                MessageBox.Show(this, "Zdjęcie zostało już dodane", "Dodaj zdjęcie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (open.ShowDialog(this) == DialogResult.OK)
            {
                
                if (File.Exists(jpg))
                {
                    
                    if (treeImageList.Images.ContainsKey(jpg))
                    {
                        treeImageList.Images[jpg].Dispose();
                        treeImageList.Images.RemoveByKey(jpg);
                        treeImageList.Images.Add(jpg, new Bitmap(jpg));
                        imagePictureBox.Image.Dispose();
                        imagePictureBox.Image = null;
                        
                    }
                }
                File.Copy(open.FileName, jpg, true);
            }
        }

        void dodajDokument_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Wybierz plik umowy, który chcesz dołączyć";
            open.Filter = "Plik Word (*.rtf)|*.rtf";
            if (open.ShowDialog(this) == DialogResult.OK)
            {
                File.Copy(open.FileName, mainTreeView.SelectedNode.FullPath + @"\_umowa.rtf", true);
            }
        }

        void refreshNode_Click(object sender, EventArgs e)
        {
            refreshNode();
        }

        void otworz_Click(object sender, EventArgs e)
        {
            string doc = mainTreeView.SelectedNode.FullPath + @"\_umowa.rtf";
            doc = mainTreeView.SelectedNode.FullPath + @"\_umowa.rtf";
            string rtf = mainTreeView.SelectedNode.FullPath + @"\_rachunek.rtf";

            ProcessStartInfo info = null;
            if (File.Exists(doc))
                info = new ProcessStartInfo("cmd.exe", string.Format("/c \"{0}\"", doc));
            else if (File.Exists(rtf))
                info = new ProcessStartInfo("cmd.exe", string.Format("/c \"{0}\"", rtf));
            else
                MessageBox.Show(this, "Brak pliku", "Otwórz", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (info != null)
            {
                info.UseShellExecute = true;
                info.LoadUserProfile = true;
                info.CreateNoWindow = true;

                Process proc = Process.Start(info);
            }
        }

        void renameNode_Click(object sender, EventArgs e)
        {
            if (mainTreeView.SelectedNode != null)
                mainTreeView.SelectedNode.BeginEdit();
        }

        public bool showConfig(TreeNode node)
        {
            object tag = null;
            tag = node.Tag;

            if (tag == null)
                return false;

            Interfejs obiekt = tag as Interfejs;
            Interfejs klon = (Interfejs)obiekt.Clone();
            //EdytorObiektu edytor = obiekt.get_Edytor();
            EdytorObiektu edytor = klon.get_Edytor();
            if (edytor.ShowDialog(this) == DialogResult.OK)
            {
                Type type = typeof(Projekt);
                node.Tag = klon;
                
                this.infoTextLabel.Text = klon.get_Info();

                switch (node.Level)
                {
                    case 0:
                        type = typeof(Projekt);
                        (klon as Projekt).DataAktualizacji = DateTime.Now;
                        break;
                    case 1:
                        type = typeof(Osoba);
                        (klon as Osoba).DataAktualizacji = DateTime.Now;
                        break;
                    case 2:
                        {
                            type = typeof(Umowa);
                            (klon as Umowa).DataAktualizacji = DateTime.Now;
                            this.m_rodzajUmowy = (klon as Umowa).Rodzaj;
                            node.ImageKey = RodzajUmowyValue.get_Value(this.m_rodzajUmowy);
                            node.SelectedImageKey = node.ImageKey;

                            manageObiekt_OnClick(node);

                            Thread.Sleep(100);

                            string plik = node.FullPath + @"\_umowa.rtf";
                            FileInfo info = new FileInfo(plik);
                            DialogResult result = DialogResult.Yes;
                            if (File.Exists(plik) && info.LastWriteTime != (klon as Umowa).DataZapisu)
                            {
                                result = MessageBox.Show(this, "Umowa została zmodyfikowana, ale plik umowy już istnieje i\n" +
                                "wygląda na to, że plik był wcześniej modyfikowany ręcznie.\n" +
                                "Czy chcesz nadpisać istniejący plik, wprowadzone zmiany zostaną utracone?",
                                "Zapis umowy", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            if (result == DialogResult.Yes)
                            {
                                StreamWriter writer = new StreamWriter(plik,
                                    false, Encoding.GetEncoding(1250));
                                writer.Write(printRichTextBoxPrintCtrl.Tag as string);
                                writer.Close();
                                info = new FileInfo(plik);
                                (klon as Umowa).DataZapisu = info.LastWriteTime;
                            }
                            checkUmowa(node);
                        }
                        break;
                    case 3:
                        {
                            (klon as Rachunek).DataAktualizacji = DateTime.Now;
                            type = typeof(Rachunek);
                            this.m_typRachunku = (klon as Rachunek).Rodzaj;
                            
                            manageObiekt_OnClick(node);

                            node.ImageKey = TypRachunkuValue.get_Value(this.m_typRachunku);
                            node.SelectedImageKey = node.ImageKey;
                            Thread.Sleep(100);
                            
                            string plik = node.FullPath + @"\_rachunek.rtf";
                            FileInfo info = new FileInfo(plik);
                            
                            DialogResult result = DialogResult.Yes;
                            if (File.Exists(plik) && info.LastWriteTime != (klon as Rachunek).DataZapisu)
                            {
                                result = MessageBox.Show(this, "Rachunek został zmodyfikowany, ale plik rachunku już istnieje i\n" +
                                "wygląda na to, że plik był wcześniej modyfikowany ręcznie.\n" +
                                "Czy chcesz nadpisać istniejący plik, wprowadzone zmiany zostaną utracone?",
                                "Zapis rachunku",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            if (result == DialogResult.Yes)
                            {
                                StreamWriter writer = new StreamWriter(plik,
                                    false, Encoding.GetEncoding(1250));
                                
                                writer.Write(printRichTextBoxPrintCtrl.Tag as string);
                                writer.Close();
                                info = new FileInfo(plik);
                                (klon as Rachunek).DataZapisu = info.LastWriteTime;
                            }
                            //mainTreeView.SelectedNode = mainTreeView.SelectedNode;
                            //sprawdz czy po dodaniu rachunku nie przekroczono wartosci umowy
                            checkUmowa(node.Parent);
                        }
                        break;
                    default:
                        type = typeof(Projekt);
                        break;
                }

                Loader.Unload(type, node.FullPath + @"\_node.xml", klon);
                return true;
            }
            return false;
        }

        void configNode_Click(object sender, EventArgs e)
        {
            if (mainTreeView.SelectedNode == null)
                return;

            showConfig(mainTreeView.SelectedNode);
        }

        public void deleteSelectedNode()
        {
            if (mainTreeView.SelectedNode == null)
                return;

            TreeNode node = mainTreeView.SelectedNode;

            string path = node.FullPath;

            //usuniecie katalogu wygeneruje zdarzenie usuniecia wezla
            //jezeli obiekt nie jest usuwany fizycznie ale tylko jest zaznaczany do usuniecia
            //to nalezy zmienic tylko plik node.xml i przechwycic zdarzenie modyfikacji tego pliku
            //a nastepnie zrobic plytka aktualizacje drzewa (tylko odczytanie plikow konfiguracyjnych node.xml)

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        void usunNode_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Jesteś pewien, że chcesz usunąć wybrany obiekt?\n" +
                (mainTreeView.SelectedNode.Tag as Interfejs).Typ + ": " + mainTreeView.SelectedNode.Name, 
                "Usuwanie", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                deleteSelectedNode();
        }

        /// <summary>
        /// dodaj do wybranej gałęzi obiekt danego typu
        /// </summary>
        public void addToSelectedNode()
        {
            //dodając nowy węzeł trzeba wybrać dla niego tymczasową nazwę
            //i go utworzyć, potem użytkownik może zmienić tą nazwę
            if (mainTreeView.SelectedNode == null)
                return;

            TreeNode node = mainTreeView.SelectedNode;

            string tmpName = Path.GetRandomFileName();
            string newName = tmpName;

            string rootPath = mainFileSystemWatcher.Path;

            //jezeli glowny wezel za nazwe ma rootPath to wystarczy uzyc tylko nazwy wezla
            string newPath = node.FullPath + Path.DirectorySeparatorChar + newName;

            //utworzenie katalogu wymusi zdarzenie utworzenia katalogu, które przechwyci FileSystemWatcher
            //i już sam zajmie się aktualizacją nowego węzła w drzewie
            //ja muszę się tylko upewnić że nowy katalog jeszcze nie istnieje

            if (!Directory.Exists(newPath))
            {
                //najpierw trzeba zalozyc blokade na dany obiekt
                //poprzez utworzenie pliku identyfikujacego aplikacje ktora go utworzyla
                string fileLock = node.FullPath + @"\" + m_lockid + ".lock";

                FileStream stream = null;
                try
                {
                    stream = File.Create(fileLock, 1024, FileOptions.DeleteOnClose);
                }
                catch (IOException)
                {
                    
                }

                Directory.CreateDirectory(newPath);

                //poczekaj na przetworzenie zdarzeń (na dodanie nowego wezla do drzewa, aby go edytowac)
                Thread.Sleep(50);
                Application.DoEvents();
                Thread.Sleep(50);
                if (node.Nodes.ContainsKey(newName))
                {
                    node.Expand();
                    node.Nodes[newName].BeginEdit();
                    if (node.Tag is Umowa)
                        checkUmowa(node);
                }else
                {
                    //jezeli nadal nie ma nowego wezla trzeba bedzie
                    //sprawdzic czy katalog rzyczywiscie zostal utworzony
                    //i do dac go recznie albo zrobic update wezla nadrzednego
                }

                stream.Close();

            }
            else
            {
                nowyKomunikat("niepoprawna nazwa: " + newPath);
            }
        }

        private bool m_fixtyp = false;

        void dodajNode_Click(object sender, EventArgs e)
        {
            
            string text = "";
            if (sender is ToolStripItem)
            {
                m_fixtyp = true;
                text = (sender as ToolStripItem).Text;
            }
            else
                m_fixtyp = false;

            //this.m_rodzajUmowy = RodzajUmowy.ODzielo;
            //this.m_typRachunku = TypRachunku.Normalny;

            if (text.StartsWith("Umowa"))
                if (text.Contains("dzieło"))
                    this.m_rodzajUmowy = RodzajUmowy.ODzielo;
                else if (text.Contains("student"))
                    this.m_rodzajUmowy = RodzajUmowy.NaZlecenieStudent;
                else if (text.Contains("zlecenie"))
                    this.m_rodzajUmowy = RodzajUmowy.NaZlecenie;
                else
                    this.m_rodzajUmowy = RodzajUmowy.ODzielo;
            else if (text.StartsWith("Rachunek"))
                if (text.Contains("normalny"))
                    this.m_typRachunku = TypRachunku.Jednorazowy;
                else if (text.Contains("częściowy"))
                    this.m_typRachunku = TypRachunku.Częściowy;
                else if (text.Contains("ostateczny"))
                    this.m_typRachunku = TypRachunku.Ostateczny;
                else
                    this.m_typRachunku = TypRachunku.Jednorazowy;

            
            addToSelectedNode();
            this.rodzajUmowyToolStripSplitButton.Text = RodzajUmowyValue.get_Value(this.m_rodzajUmowy);
            this.rodzajRachunkuToolStripSplitButton.Text = TypRachunkuValue.get_Value(this.m_typRachunku);
        }

        public void renameNode(string rootPath, string newPath, string oldPath)
        {
            //c:\\a\b\c c:\\a\b\c\d -> d
            string relativePath = oldPath.Replace(rootPath, "");

            if (mainTreeView.Nodes.Count == 0)
                mainTreeView.Nodes.Add(rootPath, rootPath, "Projekt", "Projekt");

            //szukaj wezla dla nowego obiektu
            string[] dirs = relativePath.Split(new string[] { "\\" },
                StringSplitOptions.RemoveEmptyEntries);
            
            TreeNode node = mainTreeView.Nodes[0];

            if (dirs.Length > 3)
                return;

            foreach (string dir in dirs)
            {
                if (node.Nodes.ContainsKey(dir) == false)
                {
                    //jak po drodze nie ma takiego wezla to znaczy 
                    //ze drzewo jest zepsute i trzeba je odbudowac
                    node = node.Nodes.Add(dir, dir);
                }
                else
                    node = node.Nodes[dir];
            }

            //dirs = newPath.Split('\\');
            string newName = Path.GetFileName(newPath);
            
            node.Text = newName;
            node.Name = newName;
            
            //jezeli zmieniam nazwe katalogu to musze tez zmienic nazwe w pliku _node.xml
            Thread.Sleep(100);
            customizeNode(newPath, node);

            DirectoryInfo info = new DirectoryInfo(oldPath);
            
            string msg = string.Format("zmiana nazwy: {2}->{1} ({0})",
                DateTime.Now, newName, Path.GetFileName(oldPath));
            nowyKomunikat(msg);
            noweZdarzenie(msg);
            mainTreeView.Sort();
        }

        public void deleteNode(string rootPath, string path)
        {
            //c:\\a\b\c c:\\a\b\c\d -> d
            string relativePath = path.Replace(rootPath, "");

            if (mainTreeView.Nodes.Count == 0)
                mainTreeView.Nodes.Add(rootPath, rootPath, "Projekt", "Projekt");

            //szukaj wezla dla nowego obiektu
            string[] dirs = relativePath.Split(new string[] { "\\" },
                StringSplitOptions.RemoveEmptyEntries);

            TreeNode node = mainTreeView.Nodes[0];

            if (dirs.Length > 3)
                return;

            foreach (string dir in dirs)
            {
                if (node.Nodes.ContainsKey(dir) == false)
                {
                    //nie ma teakiego wezla, drzewo jest zepsute
                    //moze trzeba je odbudowac
                    //node = node.Nodes.Add(dir, dir);
                    node = null;
                    break;
                }
                else
                    node = node.Nodes[dir];
            }

            if (node != null)
            {
                DirectoryInfo info = new DirectoryInfo(path);

                string msg = string.Format("obiekt usunięto: {2} ({1}, {0})",
                    SystemInformation.UserName, DateTime.Now, node.FullPath);
                nowyKomunikat(msg);
                noweZdarzenie(msg);

                node.Parent.Nodes.Remove(node);
            }
            else
            {
                //DirectoryInfo info = new DirectoryInfo(path);
                nowyKomunikat(string.Format("błąd usuwania: {2} ({1}, {0})", 
                    SystemInformation.UserName, DateTime.Now, path));
            }
        }

        #endregion

        #region Zdarzenia dla paneli

        private void panelHideButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.Parent.Visible = false;
        }

        private void panelMinButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b.Parent.Height > 23)
                b.Parent.Height = 23;
            else
            {
                b.Parent.Height = b.Parent.MaximumSize.Height;
                //b.Parent.Height = 100;
            }
        }

        public void setChecked(object sender, bool check)
        {
            if (sender is ToolStripButton)
                (sender as ToolStripButton).Checked = check;
            else if (sender is ToolStripMenuItem)
                (sender as ToolStripMenuItem).Checked = check;

        }

        private void panelToolStripButton_Click(object sender, EventArgs e)
        {
            //ToolStripButton b = sender as ToolStripButton;
            bool check = false;
            ToolStripItem b = sender as ToolStripItem;
            if (b.Name.StartsWith("prop"))
                check = propPanel.Visible = !propPanel.Visible;
            else if (b.Name.StartsWith("zadania"))
                zadaniaToolStripButton.Checked = zadaniaToolStripMenuItem.Checked = check = zadaniaPanel.Visible = !zadaniaPanel.Visible;
            else if (b.Name.StartsWith("zdarzenia"))
                zdarzeniaToolStripButton.Checked = zdarzeniaToolStripMenuItem.Checked = check = zdarzeniaPanel.Visible = !zdarzeniaPanel.Visible;
            else if (b.Name.StartsWith("image"))
                imageToolStripButton.Checked = imageToolStripMenuItem.Checked = check = imagePanel.Visible = !imagePanel.Visible;
            else if (b.Name.StartsWith("preview"))
                previewToolStripButton.Checked = previewToolStripMenuItem.Checked = check = printPanel.Visible = !printPanel.Visible;
            else if (b.Name.StartsWith("info"))
                infoToolStripButton.Checked = infoToolStripMenuItem.Checked = check = infoPanel.Visible = !infoPanel.Visible;
            setChecked(sender, check);
        }

        #endregion

        #region Zdarzenia dla drukowania

        private int checkPrint = 0;
        
        private void previewLinkLabel_Click(object sender, EventArgs e)
        {
            rtfPrintPreviewDialog.ShowDialog();
        }

        private void rtfPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //e.Graphics.PageUnit = GraphicsUnit.Inch;
            //e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            printRichTextBoxPrintCtrl.Print(checkPrint, printRichTextBoxPrintCtrl.TextLength, e);
        }

        private void drukujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtfPrintDialog.ShowDialog(this) == DialogResult.OK)
                rtfPrintDocument.Print();
        }

        private void ustawieniaWydrukuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtfPageSetupDialog.ShowDialog(this);
        }

        #endregion

        private void oDzieloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem b = sender as ToolStripMenuItem;
            rodzajUmowyToolStripSplitButton.Text = b.Text;
            string tag = b.Tag as string;
            this.m_rodzajUmowy = (RodzajUmowy)(int.Parse(tag));
        }

        private void normalnaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem b = sender as ToolStripMenuItem;
            rodzajRachunkuToolStripSplitButton.Text = b.Text;
            string tag = b.Tag as string;
            this.m_typRachunku = (TypRachunku)(int.Parse(tag));
        }

        private void mainToolStrip_Click(object sender, EventArgs e)
        {
            ToolStripSplitButton b = sender as ToolStripSplitButton;
            b.ShowDropDown();
        }

        public void wlasciwosciItem()
        {
            foreach (ListViewItem item in rachunkiListView.SelectedItems)
            {
                if (item.Tag is TreeNode)
                {
                    TreeNode node = item.Tag as TreeNode;
                    //rachunkiPanel.Visible = false;
                    //mainTreeView.SelectedNode = node;
                    //if (node.IsVisible == false) node.EnsureVisible();
                    if (showConfig(node))
                    {
                        mainTreeView.SelectedNode = node;
                        mainTreeView.SelectedNode = mainTreeView.Nodes[0];
                    }
                }
            }
        }

        private void rachunkiListView_DoubleClick(object sender, EventArgs e)
        {
            wlasciwosciItem();
        }

        TreeNode lastNode = null;

        private void rachunkiListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in rachunkiListView.SelectedItems)
            {
                if (item.Tag is TreeNode)
                {
                    TreeNode node = item.Tag as TreeNode;
                    //rachunkiPanel.Visible = false;
                    //mainTreeView.SelectedNode = node;
                    //node.NodeFont = new Font("Tahoma; 8.25pt; style=Bold, Underline", 8.25f);
                    
                    
                    if (node.IsVisible == false)
                        node.EnsureVisible();
                    //Font newFont = new Font(node.NodeFont, FontStyle.Bold | FontStyle.Underline);
                    node.NodeFont = selectFontDialog.Font;

                    if (lastNode != null)
                    {
                        lastNode.NodeFont = unselectFontDialog.Font;
                        
                    }
                    lastNode = node;
                    m_lastIndeks = item.Index;
                }
            }
        }

        private void wlasciwosciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wlasciwosciItem();
        }

        private void zapiszAktywneRachunkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Zapisz aktywne rachunki do pliku";
            saveDialog.Filter = "Plik XLS (*.xls)|*.xls";
            saveDialog.FileName = "Rachunki " + DateTime.Now.ToLongDateString() + ".xls";

            if (saveDialog.ShowDialog(this) == DialogResult.OK)
            {
                StreamWriter writer = 
                    new StreamWriter(
                        saveDialog.FileName,
                        false, 
                        Encoding.GetEncoding(1250));

                decimal razem = 0.0m;

                writer.WriteLine("Imię i nazwisko\tNumer umowy\tForma umowy\tRachunek\tData umowy od\tData umowy do\tWynagrodzenie\tRobota");
                foreach (ListViewItem item in rachunkiListView.Items)
                {
                    TreeNode node = item.Tag as TreeNode;
                    Rachunek rachunek = node.Tag as Rachunek;
                    Umowa umowa = node.Parent.Tag as Umowa;
                    Osoba osoba = node.Parent.Parent.Tag as Osoba;

                    razem += rachunek.Wynagrodzenie;
                    writer.WriteLine(
                        osoba.Imie + " " + osoba.Nazwisko + "\t" +
                        umowa.Numer + "\t" + 
                        RodzajUmowyValue.get_Value(umowa.Rodzaj) + "\t" +
                        rachunek.Rodzaj + "\t" + 
                        umowa.DataOd.ToShortDateString() + "\t" +
                        umowa.DataDo.ToShortDateString() + "\t" +
                        rachunek.Wynagrodzenie + "\t" + rachunek.Robota);
                }
                writer.WriteLine("\t\t\t\t\tRAZEM\t" + razem.ToString() + "\t");
                writer.Close();
            }
        }

        private void zapiszAktywneRachunkiWedługRobotyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Zapisz aktywne rachunki do pliku";
            saveDialog.Filter = "Plik XLS (*.xls)|*.xls";
            saveDialog.FileName = "Rachunki wg roboty " + DateTime.Now.ToLongDateString() + ".xls";

            if (saveDialog.ShowDialog(this) == DialogResult.OK)
            {
                StreamWriter writer =
                    new StreamWriter(
                        saveDialog.FileName,
                        false,
                        Encoding.GetEncoding(1250));

                //string roboty = "";
                Dictionary<string, int> klucze = new Dictionary<string, int>();
                
                foreach (ListViewItem item in rachunkiListView.Items)
                {
                    TreeNode node = item.Tag as TreeNode;
                    Rachunek rachunek = node.Tag as Rachunek;
                    Umowa umowa = node.Parent.Tag as Umowa;
                    //roboty += rachunek.Robota + "\t";
                    if (!klucze.ContainsKey(rachunek.Robota))
                        klucze.Add(rachunek.Robota, klucze.Count);
                }
                string[] robotyList = new string[klucze.Count];
                decimal[] robotyValue = new decimal[klucze.Count];
                
                foreach (KeyValuePair<string, int> kv in klucze)
                {
                    robotyList[kv.Value] = kv.Key;
                    robotyValue[kv.Value] = 0.0m;
                }

                string list = string.Join("\t", robotyList);

                writer.WriteLine("Imię i nazwisko\tNumer umowy\tForma umowy\tRachunek\tData umowy od\tData umowy do\t" + list);

                foreach (ListViewItem item in rachunkiListView.Items)
                {
                    
                    TreeNode node = item.Tag as TreeNode;
                    Rachunek rachunek = node.Tag as Rachunek;
                    Umowa umowa = node.Parent.Tag as Umowa;
                    Osoba osoba = node.Parent.Parent.Tag as Osoba;

                    for (int i = 0; i < robotyList.Length; i++)
                        robotyList[i] = "";
                    robotyList[klucze[rachunek.Robota]] = rachunek.Wynagrodzenie.ToString();
                    robotyValue[klucze[rachunek.Robota]] += rachunek.Wynagrodzenie;

                    list = string.Join("\t", robotyList);

                    writer.WriteLine(
                        osoba.Imie + " " + osoba.Nazwisko + "\t" +
                        umowa.Numer + "\t" + 
                        RodzajUmowyValue.get_Value(umowa.Rodzaj) + "\t" +
                        rachunek.Rodzaj + "\t" +
                        umowa.DataOd.ToShortDateString() + "\t" +
                        umowa.DataDo.ToShortDateString() + "\t" +
                        list);
                }

                writer.Write("\t\t\t\tRAZEM\t");
                decimal ogolem = 0.0m;
                foreach (decimal d in robotyValue)
                {
                    ogolem += d;
                    writer.Write(d.ToString() + "\t");
                }
                writer.WriteLine(ogolem.ToString());

                writer.Close();
            }
        }

        private void eksportujDaneOsoboweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Eksportuj dane osobowe";
            saveDialog.Filter = "Plik XLS (*.xls)|*.xls";
            saveDialog.FileName = "Dane osobowe " + DateTime.Now.ToLongDateString() + ".xls";

            if (saveDialog.ShowDialog(this) == DialogResult.OK)
            {
                StreamWriter writer =
                    new StreamWriter(
                        saveDialog.FileName,
                        false,
                        Encoding.GetEncoding(1250));

                writer.WriteLine("Imię\tNazwisko\tData urodzenia\tAdres\tPowiat\tWojewodztwo\tNIP\tPESEL\tBank\tKonto\tUrząd skarbowy");
                
                object obiekt = this.mainTreeView.SelectedNode.Tag;

                if (mainTreeView.SelectedNode != null && obiekt != null && obiekt is Projekt)
                {
                    TreeNode projNode = mainTreeView.SelectedNode;
                    Projekt projekt = obiekt as Projekt;

                    foreach (TreeNode node in projNode.Nodes)
                    {
                        Osoba osoba = node.Tag as Osoba;
                        writer.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}", 
                            osoba.Imie, osoba.Nazwisko, osoba.DataUrodzenia, osoba.Adres, osoba.Powiat, osoba.Wojewodztwo, 
                            osoba.Nip, osoba.Pesel, osoba.Bank, osoba.Konto, osoba.UrzadSkarbowy);
                    }
                }
                writer.Close();
            }
        }
    }
    
}