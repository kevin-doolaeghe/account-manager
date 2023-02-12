using backend.Models;

namespace backend.DTOs {

    public class RecordGetDto {

        public int Id { get; set; }

        public string? Description { get; set; }

        public double Amount { get; set; }

        public bool Status { get; set; }

        public DateTime Date { get; set; }

        public int TypeId { get; set; }

        public int AccountId { get; set; }

        public static RecordGetDto ToDto(Record item) {
            return new RecordGetDto {
                Id = item.Id,
                Description = item.Description,
                Amount = item.Amount,
                Status = item.Status,
                Date = item.Date,
                TypeId = item.TypeId,
                AccountId = item.AccountId,
            };
        }
    }

    public class RecordPostDto {

        public string? Description { get; set; }

        public double Amount { get; set; }

        public bool Status { get; set; }

        public DateTime Date { get; set; }

        public int TypeId { get; set; }

        public int AccountId { get; set; }

        public static Record ToItem(RecordPostDto dto) {
            return new Record {
                Description = dto.Description,
                Amount = dto.Amount,
                Status = dto.Status,
                Date = dto.Date,
                TypeId = dto.TypeId,
                AccountId = dto.AccountId,
            };
        }
    }
}