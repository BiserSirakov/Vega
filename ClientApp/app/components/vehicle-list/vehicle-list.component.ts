import { KeyValuePair } from './../../models/keyValuePair';
import { VehicleService } from './../../services/vehicle.service';
import { Vehicle } from './../../models/vehicle';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'vehicles',
    templateUrl: 'vehicle-list.component.html',
    styleUrls: ['./vehicle-list.component.css']
})

export class VehicleListComponent implements OnInit {
    vehicles: Vehicle[];
    makes: KeyValuePair[];
    filter: any = {};

    constructor(private vehicleService: VehicleService) { }

    ngOnInit() {
        this.vehicleService.getMakes()
            .subscribe(m => this.makes = m);

        this.populateVehicles();
    }

    private populateVehicles() {
        this.vehicleService.getVehicles(this.filter)
            .subscribe(v => this.vehicles = v);
    }

    onFilterChange() {
        this.populateVehicles();
    }

    resetFilter() {
        this.filter = {};
        this.onFilterChange();
    }
}