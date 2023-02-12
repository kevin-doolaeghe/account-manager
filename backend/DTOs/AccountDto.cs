using backend.Models;

namespace Backend.DTOs {

    public class AccountGetDto {

        public long Id { get; set; }

        public string? Name { get; set; }

        public static AccountGetDto ToDto(Account item) {
            return new AccountGetDto {
                Id = item.Id,
                Name = item.Name,
            };
        }
    }

    public class AccountPostDto {

        public string? Name { get; set; }

        public static Account ToItem(AccountPostDto dto) {
            return new Account {
                Name = dto.Name,
            };
        }
    }
}