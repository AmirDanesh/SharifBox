// tslint:disable-next-line: class-name
export class newsModel {
  constructor(public id: string,
    public eventBanner: string,
    public title: string,
    public content: string,
    public startDate: string,
    public endDate: string,
    public showOnLanding: boolean) { }
}
