import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-parent-svg-first',
  templateUrl: './parent-svg-first.component.html',
  styleUrls: ['./parent-svg-first.component.scss']
})
export class ParentSvgFirstComponent implements OnInit {
  @Output() svgClicked: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }
  clickSvg(event:any){
    this.svgClicked.emit(event.target.id);
   // console.log(event.target.id);
    //console.log('hiiiiiiiiiiiiiiiiiiiii')
  }
}
