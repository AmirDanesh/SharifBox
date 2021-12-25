import { Component, Inject, Input, OnInit, Renderer2 } from '@angular/core';
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';
// import {
//   trigger,
//   state,
//   style,
//   animate,
//   transition,

// } from '@angular/animations';
@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.scss']

// animations: [
//   trigger('openClose', [
//     // ...
//     state('open', style({
//       height: '200px',
//       opacity: 1,
//       backgroundColor: 'yellow'
//     })),
//     state('closed', style({
//       height: '100px',
//       opacity: 0.5,
//       backgroundColor: 'green'
//     })),
//     transition('* => closed', [
//       animate('1s')
//     ]),
//     transition('* => open', [
//       animate('0.5s')
//     ]),
//   ]),
// ]
 })
export class ToastComponent implements OnInit {
  @Input() text: string | undefined;
  constructor(public snackBarRef: MatSnackBarRef<ToastComponent> , @Inject(MAT_SNACK_BAR_DATA) public data:any) { }

  ngOnInit(): void {

  }
closeToast(){
  this.snackBarRef.dismiss()
}
// isOpen = true;

// toggle() {
//   this.isOpen = !this.isOpen;
// }

}



