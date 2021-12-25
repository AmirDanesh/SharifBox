import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.scss']
})
export class UserCardComponent implements OnInit {
  data: any;

  constructor() { }
  @Input()imageUrl: string | undefined;
  @Input()text: string | undefined;
  @Input()uniqueId: string | undefined;
  @Output() delete: EventEmitter<any> = new EventEmitter();


  ngOnInit(): void {
   //  this.imageUrl="../../../../assets/images/Profile_avatar_placeholder_large (1).png"
  }


  onDelete() {
    this.delete.emit(this.uniqueId);
}

}
