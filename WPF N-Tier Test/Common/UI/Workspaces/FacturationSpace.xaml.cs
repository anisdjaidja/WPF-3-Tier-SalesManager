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
using WPF_N_Tier_Test_Data_Access.DTOs;

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
        FactureType factureType;
        Order order;
        Person client;
        public FacturationSpace(Order _order, FactureType type)
        {
            InitializeComponent();
            order = _order;
            client = _order.Customer;
            factureType = type;
            switch (factureType)
            {
                case FactureType.Invoice:
                    TITLE = FindResource("Invoice").ToString();
                    break;
                case FactureType.Proforma:
                    TITLE = FindResource("Proforma facture").ToString();
                    break;
                case FactureType.Receipt:
                    TITLE = FindResource("Receipt").ToString();
                    break;
                default:
                    break;
            }

            //DocView.FitToWidth();
            ParseDocument();
        }
        private void ParseDocument()
        {

            CreatHeadTable();
            CreatTitleTable();
            CreatInvoiceTable();
            Total.Inlines.Add(new Run(string.Format(FindResource("TotalHT") + ": {0 :N2} " + FindResource("Currency short"), order.Amount)));
            Tva.Inlines.Add(new Run(string.Format(FindResource("Discount") + ": {0 :N0} %", order.Discount)));
            TotalandTva.Inlines.Add(new Run(string.Format(FindResource("Total") + ": {0 :N2} " + FindResource("Currency long"), order.Total)));

            displayDoc(mainDoc);
        }

        public void CreatInvoiceTable()
        {
            ProductsTable.Columns.Clear();
            ProductsTable.RowGroups.Clear();
            // Create 6 columns and add them to the table's Columns collection.

            int numberOfColumns = 4;
            if (NumberingCB.IsChecked.Value) numberOfColumns += 1;
            if (ModelCB.IsChecked.Value) numberOfColumns += 1;
            if (CategoryCB.IsChecked.Value) numberOfColumns += 1;
            if (MetricCB.IsChecked.Value) numberOfColumns += 1;

            for (int x = 0; x < numberOfColumns; x++)
            {
                ProductsTable.Columns.Add(new TableColumn());
                ProductsTable.Columns[x].Width = new GridLength(1, GridUnitType.Auto);
            }
            int firstcell = 0;
            if (NumberingCB.IsChecked.Value) { ProductsTable.Columns[0].Width = new GridLength(30, GridUnitType.Pixel); firstcell = 1; }
            ProductsTable.Columns[firstcell].Width = new GridLength(30, GridUnitType.Star);

            // Create and add an empty TableRowGroup to hold the table's Rows.
            ProductsTable.RowGroups.Add(new TableRowGroup());

            // Add Header Row
            ProductsTable.RowGroups[0].Rows.Add(new TableRow());
            // Alias the current working row for easy reference.
            TableRow currentRow = ProductsTable.RowGroups[0].Rows[0];

            // Global formatting for the header row.
            currentRow.FontSize = 12;
            currentRow.Background = Brushes.WhiteSmoke;
            currentRow.Foreground = Brushes.Gray;
            // Add cells with content to the second row.
            if (NumberingCB.IsChecked.Value)
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FindResource("Number").ToString()))));

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FindResource("Product").ToString()))));
            if (ModelCB.IsChecked.Value)
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FindResource("Model").ToString()))));
            if (CategoryCB.IsChecked.Value)
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            if (MetricCB.IsChecked.Value)
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FindResource("Metric").ToString()))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FindResource("Quantity").ToString()))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FindResource("Unit Price").ToString()))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FindResource("Total").ToString()))));
            currentRow.Cells[0].TextAlignment = TextAlignment.Left;


            // Add content in loop.
            for (int i = 0; i < order.TransactedEntities.Count; i++)
            {
                ProductsTable.RowGroups[0].Rows.Add(new TableRow());
                ProductBatch product = order.TransactedEntities[i];

                currentRow = ProductsTable.RowGroups[0].Rows[i + 1];
                // Global formatting for the row.
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;



                // Add cells with content to the third row.
                if (NumberingCB.IsChecked.Value)
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run((i + 1).ToString()))) { TextAlignment = TextAlignment.Left });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(product.ProductName))));
                if (ModelCB.IsChecked.Value)
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(product.Model))));
                if (CategoryCB.IsChecked.Value)
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(product.ProductId.ToString()))));
                if (MetricCB.IsChecked.Value)
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(product.UnitMetric))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(product.FormatedQuantity))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(string.Format("{0 :N2}", product.UnitPrice)))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(string.Format("{0 :N2}", product.TotalPrice)))));

                // Bold the first cell.

                currentRow.Cells[firstcell].FontWeight = FontWeights.Medium;
                currentRow.Cells[firstcell].FontSize = 13;
                currentRow.Cells[firstcell].TextAlignment = TextAlignment.Left;
            }




        }

        private void CreatTitleTable()
        {
            TitleTable.Columns.Clear();
            TitleTable.RowGroups.Clear();

            string orderDate = order.DateTime.Date.ToString("dd/MM/yyyy");
            if (!DateCB.IsChecked.Value) { orderDate = " . . . . "; }
            TitleTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });
            TitleTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Auto) });
            TitleTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });
            TitleTable.RowGroups.Add(new TableRowGroup());
            TitleTable.RowGroups[0].Rows.Add(new TableRow());
            TableRow TitleRow = TitleTable.RowGroups[0].Rows[0];
            Paragraph titleinfo = new();
            titleinfo.Inlines.Add(new Run(TITLE + "  ") { FontSize = 19, FontWeight = FontWeights.Bold });

            titleinfo.Inlines.Add(new Run(FindResource("Number") + ": " + order.TransactionID + " / " + FindResource("Date") + ": " + orderDate) { FontSize = 16 });

            if (SubtitleBox.Text != string.Empty)
            {
                titleinfo.Inlines.Add(new Run("\n" + SubtitleBox.Text) { FontSize = 16 });
            }

            TitleRow.Cells.Add(new TableCell() { TextAlignment = TextAlignment.Center });
            TitleRow.Cells.Add(new TableCell(titleinfo) { LineHeight = 25, TextAlignment = TextAlignment.Center, Background = Brushes.WhiteSmoke });
            TitleRow.Cells.Add(new TableCell() { TextAlignment = TextAlignment.Center });
        }
        public void CreatHeadTable()
        {

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

            orgInfo.Inlines.Add(new Run("Business") { FontSize = 14, FontWeight = FontWeights.Bold });


            currentRow.Cells.Add(new TableCell(orgInfo) { FontSize = 12, TextAlignment = TextAlignment.Left });

            Paragraph personInfo = new();

            personInfo.Inlines.Add(new Run("\n\n\n"));

            personInfo.Inlines.Add(new Run("\n" + FindResource("Customer") + ": " + client.Name));
            if (client.Address != string.Empty)
                personInfo.Inlines.Add(new Run("\n" + FindResource("Address") + ": " + client.Address));
            if (client.Company != string.Empty)
                personInfo.Inlines.Add(new Run("\n" + FindResource("Organization") + ": " + client.Company));
            personInfo.Inlines.Add(new Run("\n" + FindResource("NIF") + ": " + client.NIF));
            personInfo.Inlines.Add(new Run("\n" + FindResource("NIS") + ": " + client.NIS));
            personInfo.Inlines.Add(new Run("\n" + FindResource("N_A") + ": " + client.N_A));

            currentRow.Cells.Add(new TableCell(personInfo) { TextAlignment = TextAlignment.Justify, FontSize = 12 });





        }

        public void ParseStatements()
        {
            OpeningStatement.Inlines.Clear();
            if (OpeningStatementBox.Text == string.Empty) { OpeningStatement.LineHeight = 1; }
            else
            {
                OpeningStatement.LineHeight = 14;
                OpeningStatement.Inlines.Add(new Run(OpeningStatementBox.Text));
            }


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
            if (result == true) { WritePDF(mainDoc, dlg.FileName); }
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
            if (result == true) { WriteXPS(mainDoc, dlg.FileName); }
        }

        private void GenerateDigitWords_Click(object sender, RoutedEventArgs e)
        {
            ToWord toWord = new ToWord(Convert.ToDecimal((int)order.Total), new CurrencyInfo(CurrencyInfo.Currencies.Algeria));

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
            if (TitleTable == null) { return; }
            try { CreatTitleTable(); displayDoc(mainDoc); }
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
    }
}
