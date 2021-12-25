export class InvoiceAreaModel {
  constructor(
    public spaceId: string,
    public userName: string,
    public svgId: string,
    public amount: string,
    public startDate: string,
    public endDate: string,
    public title: string,
    public typeString: string
  ) { }
}
