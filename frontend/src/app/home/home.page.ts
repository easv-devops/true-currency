import {Component, OnInit} from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import {Currency, Service} from "./service";
import {find} from "rxjs";

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {
  protected readonly Number = Number;
  result?: number;
  indexes?: Currency[];

  constructor(private readonly fb: FormBuilder,
              private readonly service: Service) {
  }

  async ngOnInit() {
        this.indexes = await this.service.getCurrencies();
    }

  form = this.fb.group({
    source: ['', Validators.required],
    target: ['', Validators.required],
    value: ['', Validators.required]
  });

  get source() {
    return this.form.controls.source.value;
  }

  get target() {
    return this.form.controls.target.value;
  }

  get value() {
    return this.form.controls.value;
  }

  convert() {
    var source = this.form.controls.source.value;
    var target = this.form.controls.target.value;
    var value = this.form.controls.value.value;

    var sourceRateToUsd;
    var targetRateToUsd;

    for (const c of this.indexes!) {
      if(c.iso == source) {
        sourceRateToUsd = c.rateToUsd;
      }
      if(c.iso == target) {
        targetRateToUsd = c.rateToUsd;
      }
    }

    // @ts-ignore
    this.result = value?.valueOf() / sourceRateToUsd * targetRateToUsd;

  }
}
