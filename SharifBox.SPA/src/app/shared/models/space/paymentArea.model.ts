export class PaymentAreaModel {
  constructor(
    public spaceId: string,
    public userName: string,
   // public svgId: string,
    public amount: string,
    public startDate: string,
    public endDate: string,
    public startTime: string,
    public endTime: string,
    public type: string,
  ) { }
}
