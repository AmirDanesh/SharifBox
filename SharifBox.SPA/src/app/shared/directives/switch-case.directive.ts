import { Directive ,Input, Host, TemplateRef, ViewContainerRef, OnInit, DoCheck } from '@angular/core';
import { NgSwitch } from '@angular/common';

@Directive({
  selector: '[appSwitchCase]'
})
export class SwitchCaseDirective implements OnInit, DoCheck {
  private ngSwitch: any;
  private _created = false;

  @Input()
  appSwitchCase: any[] ;

  constructor(
    private viewContainer: ViewContainerRef,
    private templateRef: TemplateRef<Object>,
    @Host() ngSwitch: NgSwitch
  ) {
    this.ngSwitch = ngSwitch;
  }

  ngDoCheck(): void {
    let enforce = false;
    (this.appSwitchCase || []).forEach(value => enforce = this.ngSwitch._matchCase(value) || enforce);
    this.enforceState(enforce);
  }

  ngOnInit(): void {
   (this.appSwitchCase || []).forEach(() => this.ngSwitch._addCase());
  }


  enforceState(created: boolean) {
    if (created && !this._created) {
      this._created = true;
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else if (!created && this._created) {
      this._created = false;
      this.viewContainer.clear();
    }
  }
}
