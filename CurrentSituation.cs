using System;
using System.IO;

public class CurrentSituation
{
    private string filePath;

    public CurrentSituation(string filePath)
    {
        this.filePath = filePath;
        DisplayOrCreateCsvFile();
    }

    private void DisplayOrCreateCsvFile()
    {
        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            Console.WriteLine(content);
        }
        else
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("Date,Income,Expense,Tax");
            }
            Console.WriteLine("Date,Income,Expense,Tax");
        }
    }

    public void AddEntry(DateTime date, decimal income, decimal expense, decimal tax)
    {
        string entry = $"{date.ToString("yyyy-MM-dd")},{income},{expense},{tax}";
        File.AppendAllText(filePath, entry + "\n");
    }
}
