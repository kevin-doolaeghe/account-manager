using backend.Models;

namespace backend.DTOs {

    public class RecordGetDto {

        public int Id { get; set; }

        public string? Description { get; set; }

        public double Amount { get; set; }

        public bool Status { get; set; }

        public DateTime Date { get; set; }

        public RecordType? Type { get; set; }

        public static RecordGetDto ToDto(Record item) {
            return new RecordGetDto {
                Id = item.Id,
                Description = item.Description,
                Amount = item.Amount,
                Status = item.Status,
                Date = item.Date,
                Type = item.Type
            };
        }
    }

    public class RecordPostDto {

        public string? Description { get; set; }

        public double Amount { get; set; }

        public bool Status { get; set; }

        public DateTime Date { get; set; }

        public RecordType? Type { get; set; }

        public static Record ToItem(RecordPostDto dto) {
            return new Record {
                Description = dto.Description,
                Amount = dto.Amount,
                Status = dto.Status,
                Date = dto.Date,
                Type = dto.Type
            };
        }
    }
}