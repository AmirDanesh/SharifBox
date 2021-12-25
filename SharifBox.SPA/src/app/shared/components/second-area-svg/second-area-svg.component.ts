import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-second-area-svg',
  templateUrl: './second-area-svg.component.html',
  styleUrls: ['./second-area-svg.component.scss']
})
export class SecondAreaSvgComponent implements OnInit {
  @Output() svgClicked: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  clickSvg(event:any){
    this.svgClicked.emit(event.target.id);
    console.log(event.target.id)
  }
}
