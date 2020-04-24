using CurrencyBank.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBank.API.Utilities
{
    public static class PdfUtility
    {
        public static string GetHTMLStringAllAccountsForUser(List<AccountHistoryDto> accountHistory)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                <html>
                    <head></head>
                    <body>
                        <div class='header'><h1>Account history</h1></div>
            ");
            for (var i = 0; i < accountHistory.Count; i++)
            {
                sb.AppendFormat(@"<h2>{0}. {1}</h2>", accountHistory[i].Id, accountHistory[i].AccountNumber);
                sb.AppendFormat(@"<table align='center'>
                                    <tr>
                                        <th>Date</th>
                                        <th>Ammount</th>
                                    </tr>");
                foreach (var history in accountHistory[i].History)
                {
                    sb.AppendFormat(@"<tr>
                                        <td>{0}</td>
                                        <td>{1}</td>            
                                    </tr>", history.Timestamp, history.Ammount);
                }
                sb.Append(@"</table><br><br>");
            }
            sb.Append(@"</body></html>");
            return sb.ToString();
        }
    }
}
