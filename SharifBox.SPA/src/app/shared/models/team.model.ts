export class TeamModel {
  constructor(
    public id: string,
    public name: string,
    public managerUserId: string,
    public users: string[],
    public activityField: string,
    public description: string,
    public teamlogo: string) {

  }
}
