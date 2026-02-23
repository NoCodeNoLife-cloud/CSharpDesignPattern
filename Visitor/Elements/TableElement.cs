using Visitor.Visitors;

namespace Visitor.Elements
{
    /// <summary>
    /// Table element concrete implementation
    /// Represents tabular data in a document
    /// </summary>
    public class TableElement : IDocumentElement
    {
        public List<string> Headers { get; set; }
        public List<List<string>> Rows { get; set; }
        public string Caption { get; set; }
        public int BorderWidth { get; set; }
        public bool HasHeaderRow { get; set; }

        public TableElement()
        {
            Headers = new List<string>();
            Rows = new List<List<string>>();
            Caption = string.Empty;
            BorderWidth = 1;
            HasHeaderRow = true;
        }

        public TableElement(List<string> headers, List<List<string>> rows)
        {
            Headers = headers;
            Rows = rows;
            Caption = string.Empty;
            BorderWidth = 1;
            HasHeaderRow = true;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.VisitTableElement(this);
        }

        public void AddRow(List<string> row)
        {
            Rows.Add(row);
        }

        public void AddColumn(string header, List<string> columnData)
        {
            Headers.Add(header);
            for (int i = 0; i < Math.Min(Rows.Count, columnData.Count); i++)
            {
                Rows[i].Add(columnData[i]);
            }
        }

        public int GetRowCount()
        {
            return Rows.Count;
        }

        public int GetColumnCount()
        {
            return Math.Max(Headers.Count, Rows.Any() ? Rows[0].Count : 0);
        }

        public int GetCellCount()
        {
            return Rows.Sum(row => row.Count);
        }

        public double GetAverageRowLength()
        {
            return Rows.Count == 0 ? 0 : Rows.Average(row => row.Count);
        }

        public override string ToString()
        {
            return $"Table: {Caption} [{GetRowCount()} rows, {GetColumnCount()} columns, {GetCellCount()} cells]";
        }
    }
}