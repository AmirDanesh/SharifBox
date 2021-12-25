import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-navslide-menu',
  templateUrl: './navslide-menu.component.html',
  styleUrls: ['./navslide-menu.component.scss']
})
export class NavslideMenuComponent implements OnInit {
  form = new FormGroup({});

  constructor() { }

  ngOnInit(): void {
  }

}
