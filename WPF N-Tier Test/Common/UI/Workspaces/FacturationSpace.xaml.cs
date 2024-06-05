using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.IO.Packaging;
using WPF_N_Tier_Test.Modules.Helpers;
using WPF_N_Tier_Test.Model;

namespace WPF_N_Tier_Test.Common.UI.Workspaces
{
    /// <summary>
    /// Interaction logic for FacturationSpace.xaml
    /// </summary>
    public enum FactureType
    {
        Invoice,
        Proforma,
        Receipt,
    }
    public partial class FacturationSpace : UserControl
    {
        string TITLE;
        Order presc;
        Customer patient;
        public FacturationSpace(Order _presc, Customer _patient)
        {
            InitializeComponent();
            presc = _presc;
            patient = _patient;
            
            TITLE = FindResource("Prescription upper").ToString();

            //DocView.FitToWidth();
            ParseDocument();
        }
        private void ParseDocument()
        {
            
            CreatHeadTable();
            CreatTitleTable();
            CreatInvoiceTable();
            
                //Total.Inlines.Add(new Run(string.Format(FindResource("TotalHT") + ": {0 :N2} " + FindResource("Currency short"), presc.Amount)));
                //Tva.Inlines.Add(new Run(string.Format(FindResource("Tax") + ": {0 :N2} " + FindResource("Currency short"), presc.Tax)));
                //TotalandTva.Inlines.Add(new Run(string.Format(FindResource("Total") + ": {0 :N2} " + FindResource("Currency long"), presc.Total)));

            displayDoc(mainDoc);
        }

        public void CreatInvoiceTable()
        {

                
            ProductsTable.Columns.Clear();
            ProductsTable.RowGroups.Clear();
            // Create 6 columns and add them to the table's Columns collection.
            
            int numberOfColumns = 2;

            for (int x = 0; x < numberOfColumns; x++)
            {
                ProductsTable.Columns.Add(new TableColumn());
                ProductsTable.Columns[x].Width = new GridLength(1, GridUnitType.Auto);
            }
            int firstcell = 0;
            ProductsTable.Columns[firstcell].Width = new GridLength(30, GridUnitType.Star);

            // Create and add an empty TableRowGroup to hold the table's Rows.
            ProductsTable.RowGroups.Add(new TableRowGroup());

            // Add Header Row
            ProductsTable.RowGroups[0].Rows.Add(new TableRow());
            // Alias the current working row for easy reference.
            TableRow currentRow = ProductsTable.RowGroups[0].Rows[0];

            // Global formatting for the header row.
            currentRow.FontSize = 12;
            currentRow.Background = Brushes.Transparent;
            currentRow.Foreground = Brushes.Transparent;
            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FindResource("Medicine").ToString()))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FindResource("days").ToString()))));

            currentRow.Cells[0].TextAlignment = TextAlignment.Left;


            // Add content in loop.
            for (int i = 0; i < presc.TransactedEntities.Count; i++)
            {
                ProductsTable.RowGroups[0].Rows.Add(new TableRow());
                ProductBatch product = presc.TransactedEntities[i];

                currentRow = ProductsTable.RowGroups[0].Rows[i + 1];
                // Global formatting for the row.
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;

                // Add cells with content to the third row.
                string medLine = "";
                if (NumberingCB.IsChecked.Value)
                    medLine += (i + 1).ToString() + " - ";
                medLine += product.ProductName + "\n";
                if (product.Quantity > 0)
                    medLine += product.Quantity.ToString() + " ";
                //medLine += product.MedUnit.ToString();
                //if (MetricCB.IsChecked.Value)
                //    if (!string.IsNullOrWhiteSpace(product.DurationDesc))
                //        medLine += "/" + product.DurationDesc;
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(medLine))) { TextAlignment = TextAlignment.Left});
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(product.Duration.TotalDays.ToString() + " " + Current.FindResource("days")))) { TextAlignment = TextAlignment.Right });

                // Bold the first cell.
                
                currentRow.Cells[firstcell].FontWeight = FontWeights.Bold;
                currentRow.Cells[firstcell].FontSize = 13;
                currentRow.Cells[firstcell].TextAlignment = TextAlignment.Left;
            }




        }

        private void CreatTitleTable()
        {
            TitleTable.Columns.Clear();
            TitleTable.RowGroups.Clear();

            TitleTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });
            TitleTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Auto) });
            TitleTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });
            TitleTable.RowGroups.Add(new TableRowGroup());
            TitleTable.RowGroups[0].Rows.Add(new TableRow());
            TableRow TitleRow = TitleTable.RowGroups[0].Rows[0];
            Paragraph titleinfo = new();
            titleinfo.Inlines.Add(new Run(TITLE) { FontSize = 22, FontWeight = FontWeights.Bold });
            if (SubtitleBox.Text != string.Empty)
            {
                titleinfo.Inlines.Add(new Run("\n" + SubtitleBox.Text) { FontSize = 16 });
            }
            titleinfo.Inlines.Add(new Run("\n"));
            titleinfo.Inlines.Add(new Run("\n" + FindResource("Patient") + " : " + patient.Name) { FontSize = 14 });
            if (AgeCB.IsChecked.Value)
                titleinfo.Inlines.Add(new Run("        " + FindResource("Age") + " : " + patient.Address) { FontSize = 14 });
            if (true)
                if (GenderCB.IsChecked.Value)
                    titleinfo.Inlines.Add(new Run("        " + FindResource("Gendre") + "  : " + patient.Phone) { FontSize = 14 });

            TitleRow.Cells.Add(new TableCell() { TextAlignment = TextAlignment.Center });
            TitleRow.Cells.Add(new TableCell(titleinfo) { LineHeight = 25, TextAlignment = TextAlignment.Center, Background = Brushes.Transparent, Foreground = Brushes.DarkBlue });
            TitleRow.Cells.Add(new TableCell() { TextAlignment = TextAlignment.Center });
        }
        public void CreatHeadTable()
        {
            HeadTable.Columns.Clear();
            HeadTable.RowGroups.Clear();
            // Create 2 columns and add them to the table's Columns collection.
            HeadTable.Columns.Add(new TableColumn { Width = new GridLength(2, GridUnitType.Star) });
            HeadTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });
            // Create and add an empty TableRowGroup to hold the table's Rows.
            HeadTable.RowGroups.Add(new TableRowGroup());

            // Add Header Row
            HeadTable.RowGroups[0].Rows.Add(new TableRow());
            // Alias the current working row for easy reference.
            TableRow currentRow = HeadTable.RowGroups[0].Rows[0];
            Paragraph orgInfo = new();
            //Business business = new Business(); business.Load();
            //orgInfo.Inlines.Add(new Run(business.NameArabic) { FontSize = 14, FontWeight = FontWeights.Bold });
            //orgInfo.Inlines.Add(new Run("\n" + business.Name) { FontSize = 14, FontWeight = FontWeights.Bold });

            //if (business.Activity != string.Empty)
            //    orgInfo.Inlines.Add(new Run("\n" + business.Activity));
            //if (business.Address != string.Empty)
            //    orgInfo.Inlines.Add(new Run("\n" + FindResource("Address") + " : " + business.Address));
            //if (business.N_Ordre != string.Empty)
            //    orgInfo.Inlines.Add(new Run("\n" + "N ordre : " + business.N_Ordre));
            //if (business.Phone != string.Empty)
            //    orgInfo.Inlines.Add(new Run("\n" + FindResource("Phone") + " : " + business.Phone));
            //if (business.Email != string.Empty)
            //    orgInfo.Inlines.Add(new Run("\n" + FindResource("Email") + " : " + business.Email));
            //if (business.Fax != string.Empty)
            //    orgInfo.Inlines.Add(new Run("\n" + FindResource("Fax") + " : " + business.Fax));
            //orgInfo.Inlines.Add(new Run("\n" + FindResource("NIF")+ ": " + business.NIF) { Foreground = Brushes.Gray });
            //orgInfo.Inlines.Add(new Run("\n" + FindResource("NIS")+ ": " + business.NIS) { Foreground = Brushes.Gray });
            //orgInfo.Inlines.Add(new Run("\n" + FindResource("N_A")+ ": " + business.N_A) { Foreground = Brushes.Gray });
            //orgInfo.Inlines.Add(new Run("\n" + FindResource("NRC")+ ": " + business.NRC) { Foreground = Brushes.Gray });
            //orgInfo.Inlines.Add(new Run("\n" + FindResource("Bank Name")+ ": " + business.BankName) { Foreground = Brushes.Gray });
            //orgInfo.Inlines.Add(new Run("\n" + FindResource("Bank Account")+ ": " + business.BankAccount) { Foreground = Brushes.Gray });
            //orgInfo.Inlines.Add(new Run("\n" + FindResource("Legal form")+ ": " + business.LegalForm) { Foreground = Brushes.Gray });
            currentRow.Cells.Add(new TableCell(orgInfo) { FontSize = 12, TextAlignment = TextAlignment.Left, Foreground = Brushes.DarkBlue });

            Paragraph personInfo = new();

            //personInfo.Inlines.Add(new Run("\n\n\n"));

            string prescDate = presc.DateTime.Date.ToString("dd MMMM yyyy");
            if (!DateCB.IsChecked.Value) { prescDate = " . . . . "; }

            //personInfo.Inlines.Add(new Run("\n\n" + FindResource("Number") + " " + presc.TransactionID));
            personInfo.Inlines.Add(new Run("\n" + "Msila le : " + prescDate){Foreground = Brushes.DarkBlue});
            //if(patient.Company != string.Empty)
            //    personInfo.Inlines.Add(new Run("\n" + FindResource("Organization") + ": " + patient.Company));
            //personInfo.Inlines.Add(new Run("\n" + FindResource("NIF") + ": " + patient.NIF));
            //personInfo.Inlines.Add(new Run("\n" + FindResource("NIS") + ": " + patient.NIS));
            //personInfo.Inlines.Add(new Run("\n" + FindResource("N_A") + ": " + patient.N_A) );
            
            currentRow.Cells.Add(new TableCell(personInfo) { TextAlignment = TextAlignment.Justify, FontSize = 12});
            




        }

        public void ParseStatements()
        {
            OpeningStatement.Inlines.Clear();
            if (OpeningStatementBox.Text == string.Empty) { OpeningStatement.LineHeight = 1; }
            else { OpeningStatement.LineHeight = 14;
                OpeningStatement.Inlines.Add(new Run(OpeningStatementBox.Text)); }


            ClosingStatement.Inlines.Clear();
            if (ClosingStatementBox.Text == string.Empty) { ClosingStatement.LineHeight = 1; }
            else
            {
                ClosingStatement.LineHeight = 14;
                ClosingStatement.Inlines.Add(new Run(ClosingStatementBox.Text));
            }
            
        }
        public void ParseParties()
        {
            PartiesTable.Columns.Clear();
            PartiesTable.RowGroups.Clear();
            if (SignatureParty1.Text == string.Empty && SignatureParty2.Text == string.Empty) { return; }

            PartiesTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });
            PartiesTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });
            PartiesTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });

            PartiesTable.RowGroups.Add(new TableRowGroup());
            PartiesTable.RowGroups[0].Rows.Add(new TableRow());
            TableRow Row = PartiesTable.RowGroups[0].Rows[0];

            Paragraph Pinfo = new();
            Pinfo.Inlines.Add(new Run(SignatureParty1.Text) { FontSize = 16 });
            Paragraph P2info = new();
            P2info.Inlines.Add(new Run(SignatureParty2.Text) { FontSize = 16 });

            Row.Cells.Add(new TableCell(Pinfo) { LineHeight = 25, TextAlignment = TextAlignment.Center });
            
            Row.Cells.Add(new TableCell(P2info) { LineHeight = 25, TextAlignment = TextAlignment.Center });
            
        }

        private void displayDoc(FlowDocument flowDocument)
        {
            //generate temporary file name
            string temp = System.IO.Path.GetTempFileName();
            if (File.Exists(temp) == true)
                File.Delete(temp);

            //create a XPS document 
            XpsDocument xpsDocPrev = new XpsDocument(temp, FileAccess.ReadWrite);
            XpsDocumentWriter xpsWriterPrev = XpsDocument.CreateXpsDocumentWriter(xpsDocPrev);
            xpsWriterPrev.Write((flowDocument as IDocumentPaginatorSource).DocumentPaginator);
            DocView.Document = xpsDocPrev.GetFixedDocumentSequence();
            //close the XPS file
            xpsDocPrev.Close();
        }
        private void WriteXPS(FlowDocument flowDocument, string path)
        {
            //create a XPS document 
            XpsDocument xpsDoc = new XpsDocument(path, FileAccess.ReadWrite);
            XpsDocumentWriter xpsWriter = XpsDocument.CreateXpsDocumentWriter(xpsDoc);
            xpsWriter.Write((flowDocument as IDocumentPaginatorSource).DocumentPaginator);
            //close the XPS file
            xpsDoc.Close();
        }
        private void WritePDF(FlowDocument flowDocument, string path)
        {
            //create memory stream 
            MemoryStream lMemoryStream = new MemoryStream();
            Package package = Package.Open(lMemoryStream, FileMode.Create);

            //create a XPS document 
            XpsDocument xpsDoc = new XpsDocument(package);

            //create a XPS document writer that writes to the XPS document
            XpsDocumentWriter xpsWriter = XpsDocument.CreateXpsDocumentWriter(xpsDoc);

            //write the flow document to the XPS document
            //   use the flow documents default DocumentPaginator to do that
            xpsWriter.Write((flowDocument as IDocumentPaginatorSource).DocumentPaginator);

            xpsDoc.Close();
            package.Close();

            //convert to pdf and put in temp
            var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
            PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, path, 0);
        }

        private void ExportPDF_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "Portable Document Format (.pdf)|*.pdf"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process save file dialog box results
            if (result == true) { WritePDF(mainDoc, dlg.FileName);}
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            DocView.Print();
        }

        private void ExportXps_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Default file name
            dlg.DefaultExt = ".xps"; // Default file extension
            dlg.Filter = "XML Paper Specification (.xps)|*.xps"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process save file dialog box results
            if (result == true) { WriteXPS(mainDoc, dlg.FileName);}
        }

        private void GenerateDigitWords_Click(object sender, RoutedEventArgs e)
        {
            ToWord toWord = new ToWord(Convert.ToDecimal((int)presc.Total), new CurrencyInfo(CurrencyInfo.Currencies.Algeria));

            string arabicPrefix = "أوقفت هذه الفاتورة بمبلغ مع جميع الرسوم";
            string arabic = toWord.ConvertToArabic();
            string arabicSufix = "دينار جزائري";
            ClosingStatementBox.Text = arabicPrefix + ": " + arabic + " " + arabicSufix;
        }

        private void Statement_TextChanged(object sender, TextChangedEventArgs e)
        {
            ParseStatements(); displayDoc(mainDoc);
        }

        private void SubtitleBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TitleTable == null) { return; }
            CreatTitleTable(); displayDoc(mainDoc);
        }

        private void DateCB_Checked(object sender, RoutedEventArgs e)
        {
            if (HeadTable == null) { return; }
            try { CreatHeadTable(); displayDoc(mainDoc);}
            catch { }
        }

        private void TableOptionsChanged(object sender, RoutedEventArgs e)
        {
            if (TitleTable == null) { return; }
            try { CreatInvoiceTable(); displayDoc(mainDoc); }
            catch { }
        }

        private void Party_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PartiesTable == null) { return; }
            try { ParseParties(); displayDoc(mainDoc); }
            catch { }
        }

        private void AgeCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TitleTable == null) { return; }
            try { CreatTitleTable(); displayDoc(mainDoc); }
            catch { }
        }

        private void GenderCBCB_Checked(object sender, RoutedEventArgs e)
        {
            if (TitleTable == null) { return; }
            try { CreatTitleTable(); displayDoc(mainDoc); }
            catch { }
        }
    }
}
