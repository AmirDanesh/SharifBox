export class AreaModel{
  constructor(
              public spaceId:  string,
              public parentSvgId: string,
              public description: string,
              public typeString: string,
              public type: number,
              public capacity: number,
              public area: number,
              public numOfVideoProjector: number,
              public numOfChairs: number,
              public numOfMicrophone: number,
              public svgId: string,
              public isDisabled: boolean,
              public title:string
              ){}
}
