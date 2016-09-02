using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Rachunki
{
    class WordWraper : OfficeWraper
    {
        public static Word._Document AddDocument(Word._Application wordApp)
        {
            return wordApp.Documents.AddOld(ref ObjMissing, ref ObjMissing);
        }

        public static Word.Table GetDocumentTable(Word._Document document, int i)
        {
#if OFFICE2003
            return document.Tables[i];
#else
            return document.Tables.Item(i);
#endif
        }

        public static Word.Column GetTableColumn(Word.Table table, int i)
        {
#if OFFICE2003
            return table.Columns[i];
#else
            return table.Columns.Item(i);
#endif
        }
        
        public static Word.Border GetCellBorder(Word.Table table, int row, int col, 
            Word.WdBorderType borderType)
        {
#if OFFICE2003
            return table.Cell(row, col).Borders[borderType];
#else
            return table.Cell(row, col).Borders.Item(borderType);
#endif
        }
        #region Open / Quit / Save

        public static Word._Document WordOpen(Word._Application wordApp, object objDocName)
        {
            return wordApp.Documents.Open(
                ref objDocName,    //FileName
                ref ObjMissing,   //ConfirmVersions
                ref ObjTrue,      //ReadOnly
                ref ObjMissing,   //AddToRecentFiles
                ref ObjMissing,   //PasswordDocument
                ref ObjMissing,   //PasswordTemplate
                ref ObjMissing,   //Revert
                ref ObjMissing,   //WritePasswordDocument
                ref ObjMissing,   //WritePasswordTemplate
                ref ObjMissing,   //Format
                ref ObjMissing,   //Enconding
                ref ObjMissing    //Visible
#if OFFICE2003   
                ,ref ObjMissing,   //OpenAndRepair
                ref ObjMissing,   //DocumentDirection
                ref ObjMissing,   //NoEncodingDialog
                ref ObjMissing    //XMLTransform*/
#endif
            );
        }

        public static Word._Document WordOpenForWrite(Word._Application wordApp, object objDocName)
        {
            return wordApp.Documents.Open(
                ref objDocName,    //FileName
                ref ObjMissing,   //ConfirmVersions
                ref ObjFalse,      //ReadOnly
                ref ObjMissing,   //AddToRecentFiles
                ref ObjMissing,   //PasswordDocument
                ref ObjMissing,   //PasswordTemplate
                ref ObjMissing,   //Revert
                ref ObjMissing,   //WritePasswordDocument
                ref ObjMissing,   //WritePasswordTemplate
                ref ObjMissing,   //Format
                ref ObjMissing,   //Enconding
                ref ObjMissing    //Visible
#if OFFICE2003
                , ref ObjMissing,   //OpenAndRepair
                ref ObjMissing,   //DocumentDirection
                ref ObjMissing,   //NoEncodingDialog
                ref ObjMissing    //XMLTransform*/
#endif
            );
        }

        public static void WordQuit(Word._Application wordApp)
        {
            wordApp.Quit(ref ObjMissing, ref ObjMissing, ref ObjMissing);
        }

        public static void WordSaveAs(Word._Document doc, string fileName)
        {
            object name = fileName;
            doc.SaveAs(ref name, ref ObjMissing, ref ObjMissing, ref ObjMissing,
                ref ObjMissing, ref ObjMissing, ref ObjMissing,
                ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing
#if OFFICE2003
                ,ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing
#endif
);
        }

        public static void WordSaveAsRtf(Word._Document doc, string fileName)
        {
            object name = fileName;
            object format = Word.WdSaveFormat.wdFormatRTF;
            doc.SaveAs(ref name, ref format, ref ObjMissing, ref ObjMissing,
                ref ObjMissing, ref ObjMissing, ref ObjMissing,
                ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing
#if OFFICE2003
, ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing
#endif
);
        }

        #endregion

        #region Import / Export
        /*
        public static List<EnvelopeSource> WordImportSourceEnvelopes(string fileName)
        {
            List<EnvelopeSource> sources = new List<EnvelopeSource>();
            Word._Application objApp = new Word.Application();
            objApp.Visible = false;
            objApp.ScreenUpdating = false;

            try
            {
                Word._Document objDoc = null;

                object objDocName = fileName;

                objDoc = UtilWord.WordOpen(objApp, objDocName);

                int nCountRows = 0;
                foreach (Word.Table t in objDoc.Tables)
                {
                    nCountRows += t.Rows.Count;
                }

                string sColId = null, sColWho = null, sColAdr = null;
                string sColIdPrev = null, sColIdBase;

                foreach (Word.Table t in objDoc.Tables)
                {
                    int tableFirstRow = 3;
                    for (int i = tableFirstRow; i <= t.Rows.Count; i++)
                    {
                        try
                        {
                            EnvelopeSource source = new EnvelopeSource();

                            sColIdBase = t.Cell(i, 1).Range.Text;
                            sColId = Util.TrimId(sColIdBase);
                            sColWho = t.Cell(i, 2).Range.Text;
                            sColAdr = t.Cell(i, 3).Range.Text;

                            if (string.IsNullOrEmpty(sColId))
                                sColId = sColIdPrev;

                            source.Adres = sColAdr;
                            source.Adresat = sColWho;
                            source.Id = sColId;

                            sources.Add(source);

                            sColIdPrev = sColId;
                        }
                        catch (COMException)
                        {
                            if (i > t.Rows.Count)
                            {
                                break;
                            }
                        }
                    }
                }
                objDoc.Close(ref ObjMissing, ref ObjMissing, ref ObjMissing);
            }
            catch (COMException)
            {
            }
            finally
            {
                objApp.Quit(ref ObjMissing, ref ObjMissing, ref ObjMissing);
            }
            return sources;
        }
        */
        /*
        public static void WordSaveEnvelopes(string fileName, List<Envelope> envelopes)
        {
            Word._Application app = new Word.Application();
            app.Visible = true;
            Word._Document doc =
                app.Documents.Add(
                    ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing);

            Word.Table tab = null;
            if (envelopes.Count > 0)
                tab = doc.Tables.Add(app.Selection.Range,
                    envelopes.Count, 3, ref ObjMissing, ref ObjMissing);

            int nRow = 1;
            foreach (Envelope envelope in envelopes)
            {
                tab.Cell(nRow, 1).Range.Text = envelope.Id;
                if (envelope.Text != null)
                {
                    tab.Cell(nRow, 2).Range.Text = envelope.Adresat;
                    tab.Cell(nRow, 3).Range.Text = envelope.Adres;
                }
                else
                {
                    tab.Cell(nRow, 2).Range.Text = envelope.Adresat;
                    tab.Cell(nRow, 3).Range.Text = envelope.Adres;
                }

                nRow++;
            }
            WordSaveAs(doc, fileName);
        }
        */
        #endregion

        #region Notification
        /*
        public static string WordShowNotification(string notification, string adresat, string adres)
        {
            Word._Application wordApp = new Word.Application();
            wordApp.Visible = true;

            bool found = false;
            string message = "";
            try
            {
                Word._Document objDoc = null;
                object objStrDoc = notification;
                objDoc = WordOpen(wordApp, objStrDoc);

                wordApp.Selection.Find.ClearFormatting();
                wordApp.Selection.Find.Replacement.ClearFormatting();

                object objMatchText = Properties.Settings.Default.AddressPattern;
                object objReplaceWith = adresat;

                found = wordApp.Selection.Find.ExecuteOld(ref objMatchText,
                    ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing,
                    ref ObjTrue, ref ObjMissing, ref ObjMissing, ref objReplaceWith, ref ObjMissing);

                if (found)
                {
                    if (adresat != "")
                        wordApp.Selection.TypeText(adresat);

                    string[] rows = adres.Split((char)13);
                    foreach (string row in rows)
                    {
                        wordApp.Selection.TypeParagraph();
                        wordApp.Selection.TypeText(Util.TrimAdres(row));
                    }
                }
            }
            catch (COMException com)
            {
                return com.Message;
            }
            finally
            {
            }
            return found ? message :
                "Nie znaleziono wyra¿enia do zamiany.\nZawiadomienie powinno zawieraæ ci¹g do podmiany " +
                Properties.Settings.Default.AddressPattern;
        }
        */
        /*
        public static string WordPrintNotification(string notification, string adresat, string adres)
        {
            Word._Application wordApp = new Word.Application();
            wordApp.Visible = false;

            bool found = false;
            string message = "";
            try
            {
                Word._Document objDoc = null;
                object objStrDoc = notification;
                objDoc = WordOpen(wordApp, objStrDoc);

                wordApp.Selection.Find.ClearFormatting();
                wordApp.Selection.Find.Replacement.ClearFormatting();

                object objMatchText = Properties.Settings.Default.AddressPattern;
                object objReplaceWith = adresat;

                found = wordApp.Selection.Find.ExecuteOld(ref objMatchText,
                    ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing,
                    ref ObjTrue, ref ObjMissing, ref ObjMissing, ref objReplaceWith, ref ObjMissing);

                if (found)
                {
                    if (adresat != "")
                        wordApp.Selection.TypeText(adresat);

                    string[] rows = adres.Split((char)13);
                    foreach (string row in rows)
                    {
                        wordApp.Selection.TypeParagraph();
                        wordApp.Selection.TypeText(Util.TrimAdres(row));
                    }

                    objDoc.PrintOutOld(ref ObjTrue, ref ObjMissing, ref ObjMissing,
                        ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing,
                        ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing,
                        ref ObjMissing);
                }

                objDoc.Close(ref ObjFalse, ref ObjMissing, ref ObjMissing);
            }
            catch (COMException com)
            {
                message = com.Message;
            }
            finally
            {
                while (wordApp.BackgroundPrintingStatus > 0)
                {
                    continue;
                }
                WordQuit(wordApp);
            }
            return found ? message :
                "Nie znaleziono wyra¿enia do zamiany.\nZawiadomienie powinno zawieraæ ci¹g do podmiany " +
                Properties.Settings.Default.AddressPattern;
        }
        */
        public static string WordSaveNotificationAsRtf(string notification, string adresat, string adres)
        {
            Word._Application wordApp = new Word.Application();
            wordApp.Visible = false;

            string message = "";
            try
            {
                Word._Document objDoc = null;
                object objStrDoc = notification;
                objDoc = WordOpenForWrite(wordApp, objStrDoc);

                wordApp.Selection.Find.ClearFormatting();
                wordApp.Selection.Find.Replacement.ClearFormatting();

                object objMatchText = "";// Properties.Settings.Default.AddressPattern;
                object objReplaceWith = adresat;

                wordApp.Selection.Find.ExecuteOld(ref objMatchText,
                    ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing, ref ObjMissing,
                    ref ObjTrue, ref ObjMissing, ref ObjMissing, ref objReplaceWith, ref ObjMissing);

                if (adresat != "")
                    wordApp.Selection.TypeText(adresat);

                string[] rows = adres.Split((char)13);
                foreach (string row in rows)
                {
                    wordApp.Selection.TypeParagraph();
                    //wordApp.Selection.TypeText(Util.TrimAdres(row));
                }

                WordSaveAsRtf(objDoc, notification + ".rtf");

                objDoc.Close(ref ObjTrue, ref ObjMissing, ref ObjMissing);
            }
            catch (COMException com)
            {
                message = com.Message;
            }
            finally
            {
                WordQuit(wordApp);
            }
            return message;
        }

        #endregion

        public static bool WordImportUmowa(string fileName, ref string rodzaj, ref string numer, ref string konto, ref string osoba)
        {
            Word._Application wordApp = new Word.Application();
            wordApp.Visible = true;
            wordApp.ScreenUpdating = true;

            object wdStory = Word.WdUnits.wdStory;
            object wdLine = Word.WdUnits.wdLine;
            object wdMove = Word.WdMovementType.wdMove;
            object count1 = (object)1;
            object wdExtend = Word.WdMovementType.wdExtend;

            try
            {
                Word._Document objDoc = null;
                object objDocName = fileName;
                objDoc = WordOpen(wordApp, objDocName);

                string text = "";
                int granica;
                bool isend = false;

                granica = 0;
                while (true)//while szukaj kopert
                {
                    wordApp.Selection.HomeKey(ref wdLine, ref wdMove);
                    wordApp.Selection.EndKey(ref wdLine, ref wdExtend);

                    if (++granica > 30)
                        isend = true;

                    //if(wordApp.Selection.End == ((Word.Range)objDoc.Range).End)
                    //  isend = true;

                    text = wordApp.Selection.Text.Trim();

                    if (text.Contains("UMOWA"))
                    {
                        if (text.Contains("DZIE£O"))
                            rodzaj = "Umowa o dzie³o";
                        else if (text.Contains("ZLECENIE"))
                        {
                            rodzaj = "Umowa na zlecenie";
                            if (text.Contains("student"))
                            {
                                rodzaj = "Umowa na zlecenie - student";
                            }
                        }
                        //numer = text.Substring(text.Length - 14);
                        numer = text;
                    }
                    else if (text.Contains("konta"))
                        konto = text;

                    if (text.Contains(osoba))
                    {
                        osoba = text;
                    }

                    wordApp.Selection.HomeKey(ref wdLine, ref wdMove);
                    wordApp.Selection.MoveDown(ref wdLine, ref count1, ref wdMove);

                    if (isend)
                        break;
                }//end while szukaj kopert              

                objDoc.Close(ref ObjMissing, ref ObjMissing, ref ObjMissing);
            }
            catch (COMException)
            {
                return false;
            }
            finally
            {
                wordApp.Quit(ref ObjMissing, ref ObjMissing, ref ObjMissing);
            }
            return true;
        }
    }
}
