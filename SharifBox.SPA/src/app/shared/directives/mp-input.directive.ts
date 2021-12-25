import { Directive, OnInit, HostBinding, HostListener, Renderer2, ElementRef } from '@angular/core';

@Directive({
  selector: '[MpInput]'
})
export class MpInputDirective implements OnInit {


// @HostBinding('style.backgroundColor') backgroundColor:string ='transparent';
   constructor(private elRef:ElementRef, private renderer:Renderer2) { }

ngOnInit(){
  this.renderer.setStyle(this.elRef.nativeElement, 'background','blue')
}

//@HostListener('mouseenter') mouseover(eventData:Event){
  // this.renderer.setStyle(this.elRef.nativeElement, 'background','blue')
  // this.backgroundColor='blue'
 // console.log('hhh')
//}

//@HostListener('mouseleave') mouseleave(eventData:Event){
  // this.renderer.setStyle(this.elRef.nativeElement, 'background','blue')
  // this.backgroundColor='transparent'

//}

}
