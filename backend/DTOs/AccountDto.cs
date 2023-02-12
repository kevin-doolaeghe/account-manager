using backend.Models;

namespace backend.DTOs {

    public class AccountGetDto {

        public int Id { get; set; }

        public string? Name { get; set; }

        public int UserId { get; set; }

        public static AccountGetDto ToDto(Account item) {
            return new AccountGetDto {
                Id = item.Id,
                Name = item.Name,
                UserId = item.UserId,
            };
        }
    }

    public class AccountPostDto {

        public string? Name { get; set; }

        public int UserId { get; set; }

        public static Account ToItem(AccountPostDto dto) {
            return new Account {
                Name = dto.Name,
                UserId = dto.UserId,
            };
        }
    }
}