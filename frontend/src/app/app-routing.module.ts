import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import {HomePage} from "./home/home.page";
import {AppComponent} from "./app.component";
import {HomePageModule} from "./home/home.module";

const routes: Routes = [
  {
    path: 'home',
    component: HomePage
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { useHash: true })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
