import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-child-svg',
  templateUrl: './child-svg.component.html',
  styleUrls: ['./child-svg.component.scss']
})
export class ChildSvgComponent implements OnInit {

  @Output() svgClicked: EventEmitter<any> = new EventEmitter();
  @Input() data:  any[] = [];
  constructor() { }

  ngOnInit(): void {
  }


  clickSvg(event:any){
    this.svgClicked.emit(event.target.id);
    console.log(event.target.id)
  }

  // getSvg(id:any){
  //   if(this.data){
  //     return this.data.filter(x=>x.svgId == id)[0];
  //   }
  //   return undefined;
  // }

}
