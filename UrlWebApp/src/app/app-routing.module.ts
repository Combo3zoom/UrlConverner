import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {ShortUrlsComponent} from "./short-urls/short-urls.component";
import {RegisterComponent} from "./register/register.component";
import {ShortUrlDetailsComponent} from "./short-url-details/short-url-details.component";

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'short-urls', component: ShortUrlsComponent },
  { path: 'short-url-details/:id', component: ShortUrlDetailsComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
