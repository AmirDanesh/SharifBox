import { MatSnackBar } from '@angular/material/snack-bar';
import { AreaService } from './../../../shared/services/area.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';

@Component({
  selector: 'app-svg-container',
  templateUrl: './container-svg.component.html',
  styleUrls: ['./container-svg.component.scss']
})
export class ContainerSvgComponent implements OnInit {
id:any;
@Output() reset: EventEmitter<string> = new EventEmitter()
@Output() modelChanged: EventEmitter<any> = new EventEmitter()
parentSvgId:string ='';
model: any
jigul:any
  constructor(private areaService:AreaService, private _snackBar:MatSnackBar) { }

  ngOnInit(): void {
  }

  getSelectedArea(event:any){

  this.id = event
   //console.log(this.id)
    this.areaService.getSelectedArea(this.id).subscribe(
      (nxt: any) => {
      this.modelChanged.emit(nxt)
      //console.log(nxt)
      this.jigul = nxt.parentId;
      console.log(this.jigul)

      },
      (err) => {
        this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: err.error
          }
        })
        this.reset.emit(event)
        this.modelChanged.emit({svgId:this.id, parentSvgId:this.parentSvgId})
      }
    );
  }

  // getFirstChild(){
  //   this.currentSvg = 1;
  //   console.log("im in the first one")
  // }

  // getSecondChild(){
  //   this.currentSvg = 2;
  //   console.log("im in the first one")
  // }

  moveIn(event:any){
   this.parentSvgId = event;
   console.log(event)
  }

  moveOut(){
    //this.parentSvgId = event
    console.log(this.parentSvgId)
   }

}
