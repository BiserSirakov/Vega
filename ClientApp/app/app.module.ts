import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';

import { AppComponent } from './components/app/app.component'
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PaginationComponent } from './components/shared/pagination/pagination.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';

import * as Raven from 'raven-js';
import { ToastyModule } from 'ng2-toasty';

import { VehicleService } from './services/vehicle.service';
import { PhotoService } from './services/photo.service';
import { AppErrorHandler } from "./app.error-handler";
import { BrowserXhr } from '@angular/http';
import { CustomBrowserXhr } from './services/customBrowserXhr.service';
import { ProgressService } from './services/progress.service';

Raven.config('https://0a6243957a294c89a1b11a58f4ca647c@sentry.io/170106').install();

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        NavMenuComponent,
        PaginationComponent,
        ViewVehicleComponent,
        VehicleFormComponent,
        VehicleListComponent
    ],
    imports: [
        FormsModule,
        ToastyModule.forRoot(),
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'vehicles/new', component: VehicleFormComponent },
            { path: 'vehicles/edit/:id', component: VehicleFormComponent },
            { path: 'vehicles/:id', component: ViewVehicleComponent },
            { path: 'vehicles', component: VehicleListComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler },
        { provide: BrowserXhr, useClass: CustomBrowserXhr },   
        VehicleService,
        PhotoService,
        ProgressService
    ]
})
export class AppModule {
}
