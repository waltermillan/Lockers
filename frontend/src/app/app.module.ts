import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { LockerCrudComponentComponent } from './locker-crud/locker-crud-component.component';
import { LockerUpdateComponent } from './locker-update/locker-update.component';
import { LocationCrudComponent } from './location-crud/location-crud.component';
import { PriceCrudComponent } from './price-crud/price-crud.component';
import { LocationUpdateComponent } from './location-update/location-update.component';
import { PriceUpdateComponent } from './price-update/price-update.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    LockerCrudComponentComponent,
    LockerUpdateComponent,
    LocationCrudComponent,
    PriceCrudComponent,
    LocationUpdateComponent,
    PriceUpdateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    MatDialogModule
  ],
  providers: [
    provideHttpClient(withInterceptorsFromDi(), withFetch())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
