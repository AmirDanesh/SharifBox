import { Component, forwardRef, Input, ViewEncapsulation } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';


@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputComponent),
      multi: true
    }
  ],
  encapsulation: ViewEncapsulation.None // Use to disable CSS Encapsulation for this component
})
export class InputComponent implements ControlValueAccessor {
  [x: string]: any;

  constructor() { }
  @Input() label: string | undefined;
  @Input() type: string | undefined;
  // @Input() textArea: boolean | undefined;

  InputValue: any;
  // tslint:disable-next-line: typedef
  writeValue(value: any) {
    this.InputValue = value;
  }
  propagateChange = (_: any) => { };

  // tslint:disable-next-line: typedef
  registerOnChange(fn: (_: any) => void) {
    this.propagateChange = fn;
  }

  // tslint:disable-next-line: typedef
  registerOnTouched(fn: any) {
    this.onTouched = fn;
  }
  // tslint:disable-next-line: typedef
  onChange(event: any) {
    this.InputValue = event.target.value;
    this.propagateChange(this.InputValue);
  }
  // tslint:disable-next-line: typedef
  public onTouched: any = () => { /*Empty*/ };
}
