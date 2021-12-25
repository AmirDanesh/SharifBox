import { PaymentAreaModel } from './../../../shared/models/space/paymentArea.model';
import { LocalStorageService } from 'ngx-webstorage';
import { AreaService } from './../../../shared/services/area.service';
import { Component, Input, OnInit } from '@angular/core';


@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent implements OnInit {

  @Input() model: any | undefined;
  paymentModel: PaymentAreaModel | undefined;
  phoneNumber: any;
  constructor(private areaService: AreaService, private localStorageSrvice: LocalStorageService) {
    this.phoneNumber = this.localStorageSrvice.retrieve('phoneNumber');
   // console.log(this.phoneNumber)
  }

  ngOnInit(): void {
    // this.model = this.locationService.getState() as InvoiceAreaModel;

    // )
  }

  makePayment() {
    this.paymentModel = {
      spaceId: this.model.spaceId,
      userName: this.localStorageSrvice.retrieve('phoneNumber'),
      //svgId: this.model.svgId,
      amount: this.model.amount,
      startDate: this.model.startDate,
      endDate: this.model.endDate,
      startTime: this.model.startTime,
      endTime: this.model.endTime,
      type: this.model.type
    }

    console.log(this.paymentModel)
    if (this.model)
      this.areaService.makePayment(this.paymentModel).subscribe(nxt => {
        location.assign(nxt);
        console.log(nxt);
      });
  }
}
