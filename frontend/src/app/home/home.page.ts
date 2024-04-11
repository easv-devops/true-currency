import {Component} from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {
  protected readonly Number = Number;
  result?: number;
  indexes?: number[] = [1, 0.93];

  constructor(private readonly fb: FormBuilder) {}

  form = this.fb.group({
    source: ['', Validators.required],
    target: ['', Validators.required],
    value: ['', Validators.required]
  });

  get source() {
    return this.form.controls.source;
  }

  get target() {
    return this.form.controls.target;
  }

  get value() {
    return this.form.controls.value;
  }

  convert() {
    var source = this.form.controls.source.value;
    var target = this.form.controls.target.value;
    var value = this.form.controls.value.value;

    // @ts-ignore
    this.result = value?.valueOf() / source?.valueOf() * target?.valueOf();

    console.log(value + ' / ' + source + ' * ' + target + ' = ')
    console.log(this.result)
  }
}
