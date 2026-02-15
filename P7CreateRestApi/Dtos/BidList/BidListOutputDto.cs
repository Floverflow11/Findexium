namespace P7CreateRestApi.Dtos.BidList;

public record BidListOutputDto(int Id, string Account, string BidType, double? BidQuantity);