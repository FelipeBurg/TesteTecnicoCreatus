using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.IO;
using System.Collections.Generic;
using QuestPDF.Helpers;

namespace TesteTecnicoCreatus.Users
{
    public class Relatory
    {
        private readonly string _title;
        private readonly List<UserDetailsDto> _users;

        public Relatory(string title, List<UserDetailsDto> users)
        {
            _title = title;
            _users = users;
        }

        public byte[] GeneratePdf()
        {
            using var stream = new MemoryStream();
            
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    
                    page.Header()
                        .Text(_title)
                        .FontSize(40)
                        .Bold()
                        .AlignCenter();
                    
                    page.Content()
                        .Column(column =>
                        {
                            column.Spacing(15);
                            
                            foreach (var user in _users)
                            {
                                column.Item().Text($"Name: {user.Name}");
                                column.Item().Text($"Email: {user.Email}");
                                column.Item().Text($"Level: {user.Level}");
                                column.Item().Text(" "); // Add a blank line between users
                            }
                        });
                    
                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Página ");
                            text.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf(stream);
            
            return stream.ToArray();
        }
    }
}
