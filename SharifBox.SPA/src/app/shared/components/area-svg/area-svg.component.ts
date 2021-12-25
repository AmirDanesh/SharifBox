import { reserveTimeModel } from './../../models/space/reserveTime.model';
import { Router } from '@angular/router';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-area-svg',
  templateUrl: './area-svg.component.html',
  styleUrls: ['./area-svg.component.scss']
})
export class AreaSvgComponent implements OnInit {

  constructor(private router:Router) { }
  @Output() svgClicked: EventEmitter<any> = new EventEmitter();
  @Input() data:  any[] = [];
// @Input() status: boolean | undefined;


  ngOnInit(): void {
  }
  red = '#600';
  blue = '#006';
  // clickPurpleSvg(event:any){
  //   this.svgClicked.emit(event.target.id);
    //this.router.navigate(['/child'])
  //   console.log(event.target.id)
  // }
  clickSvg(event:any){
    this.svgClicked.emit(event.target.id);
    console.log(event.target.id)
  }

  getSvg(id:any){
    if(this.data){
      //console.log(id, ': ', this.data.filter(x=>x.svgId == id)[0])
      return this.data.filter(x=>x.svgId == id)[0];
    }
    return undefined;
  }



}
