import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-parent-svg-second',
  templateUrl: './parent-svg-second.component.html',
  styleUrls: ['./parent-svg-second.component.scss']
})
export class ParentSvgSecondComponent implements OnInit {
  @Output() svgClicked: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }
  clickSvg(event:any){
    this.svgClicked.emit(event.target.id);
   // console.log(event.target.id);
  }
}
