import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-readonly-input',
  templateUrl: './readonly-input.component.html',
  styleUrls: ['./readonly-input.component.scss']
})
export class ReadonlyInputComponent implements OnInit {
  @Input()label: string | undefined;
  @Input()value: string | undefined;


  constructor() { }

  ngOnInit(): void {

  }

}
