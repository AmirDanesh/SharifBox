import { OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { AreaService } from './../../../shared/services/area.service';
import { DropdownModel } from './../../../shared/models/dropdown.model';
import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { takeUntil } from 'rxjs/operators';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';
import { Output } from '@angular/core';

@Component({
  selector: 'app-sidebar-svg',
  templateUrl: './sidebar-svg.component.html',
  styleUrls: ['./sidebar-svg.component.scss']
})
export class SidebarSvgComponent implements OnInit, OnDestroy {
  areaForm: FormGroup = new FormGroup({});
  @Input() svgId = '';
  @Input() spaceId = '';
  @Input() parentSvgId = '';
  @Input() resetter: EventEmitter<string> = new EventEmitter();
  @Input() model: EventEmitter<any> = new EventEmitter();
  @Output() moveInClicked: EventEmitter<any> = new EventEmitter();
  @Output() moveOutClicked: EventEmitter<any> = new EventEmitter();

  $destroy: Subject<void> = new Subject();
  typeDropDown: DropdownModel[] = [];
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  constructor(
    private areaService: AreaService,
    private _snackBar: MatSnackBar,
    private fb: FormBuilder) {
  }

  ngOnDestroy(): void {
    this.$destroy.next();
    this.$destroy.complete();
  }

  ngOnInit(): void {
    this.areaForm = this.fb.group({
      svgId: [],
      spaceId: [],
      'title': [, Validators.required],
      'description': [],
      'type': [, Validators.required],
      'capacity': [0],
      'area': [0],
      'numOfVideoProjector': [0],
      'numOfChairs': [0],
      'numOfMicrophone': [0],
      parentSvgId: []
    });
    // console.log(this.id)
    this.resetter.pipe(takeUntil(this.$destroy))
      .subscribe(
        (spaceId: string) => this.areaForm.reset({ spaceId })
      );
    this.areaService.getType()
      .subscribe(
        nxt => {
          this.typeDropDown = nxt,
            console.log(nxt)
        });

    this.model.pipe(takeUntil(this.$destroy)).subscribe(model => {
      this.spaceId = model.spaceId;
      this.parentSvgId = model.parentSvgId;
      this.areaForm.patchValue(model);
     // console.log('areaform :' + JSON.stringify(this.areaForm.value))
     // console.log('model :' +  JSON.stringify(model))
    }
    );
  }

  submitArea() {
    this.areaForm.markAllAsTouched();
    if (!this.areaForm.valid)
      return;
    //  this.resetter.emit()
    if (this.spaceId) {
      console.log('spaceId=' + this.spaceId)
      console.log('im in update')
      this.areaService.updateArea(this.areaForm.value, this.spaceId).subscribe(nxt => {
        console.log(nxt)
        this.areaForm.patchValue(nxt);

        this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: 'درخواست با موفقیت انجام شد'
          }
        });
        this.model.emit({})
        this.areaForm.reset();
      },
        err => {
          this.model.emit({})
          this._snackBar.openFromComponent(ToastComponent, {
            data: {
              text: err.error
            }
          })
        })
    }

    else {
      this.areaService.addArea(this.areaForm.value).subscribe(
        (nxt) => {
          console.log('im in add')
          console.log(this.areaForm.value)
          this._snackBar.openFromComponent(ToastComponent, {
            data: {
              text: 'درخواست با موفقیت انجام شد'
            }
          });
        //  this.areaForm.reset();
          console.log(this.model);
          this.model.emit({})
          console.log(this.model);
        }
        , err => this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: err.error
          }
        }))
    }
  }

  get spaceType() {
    return this.areaForm.get('type')?.value;
  }

  moveIn() {
    this.parentSvgId = this.areaForm.controls.svgId.value;
    this.moveInClicked.emit(this.parentSvgId)
    console.log(this.parentSvgId)
    this.areaForm.reset()
  }

  moveOut(event: any) {
    this.moveOutClicked.emit(event)
    this.areaForm.reset()

  }
}
