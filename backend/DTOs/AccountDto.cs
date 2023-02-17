using backend.Models;

namespace backend.DTOs {

    public class AccountGetDto {

        public long AccountId { get; set; }

        public string? Name { get; set; }

        public long UserId { get; set; }

        public static AccountGetDto ToDto(Account item) {
            return new AccountGetDto {
                AccountId = item.AccountId,
                Name = item.Name,
                UserId = item.UserId,
            };
        }
    }

    public class AccountPostDto {

        public string? Name { get; set; }

        public long UserId { get; set; }

        public static Account ToItem(AccountPostDto dto) {
            return new Account {
                Name = dto.Name,
                UserId = dto.UserId,
            };
        }
    }
}
