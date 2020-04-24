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
        public static string GetHTMLString()
        {
            var sb = new StringBuilder();
            sb.Append(@"
                <html>
                    <head></head>
                    <body>
                        <div class='header'><h1>Account history</h1></div>
                            <table align='center'>
                                <tr>
                                    <th>Date</th>
                                    <th>Ammount</th>
                                </tr>
                    </body>
                </html>
            ");
            for (int i = 0; i < 10; i++)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                  </tr>", "test", "test1");
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}
