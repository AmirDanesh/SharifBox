import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-btn',
  templateUrl: './btn.component.html',
  styleUrls: ['./btn.component.scss']
})
export class BtnComponent implements OnInit {

  @Input() label: string | undefined;
  @Output() onClick = new EventEmitter<any>();
  @Input() type: string | undefined;

  onClickButton(event: any) {
    this.onClick.emit(event);
  }

  constructor() { }

  ngOnInit(): void {
  }

}
