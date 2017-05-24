import { ToastyService } from 'ng2-toasty';
import * as _ from 'underscore';
import { Vehicle } from './../../models/vehicle';
import { SaveVehicle } from './../../models/saveVehicle';
import { Observable } from "rxjs/Observable";
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import 'rxjs/add/Observable/forkJoin';

import { VehicleService } from '../../services/vehicle.service';

@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {

    makes: any[];
    models: any[];
    features: any[];
    vehicle: SaveVehicle = {
        id: 0,
        makeId: 0,
        modelId: 0,
        isRegistered: false,
        features: [],
        contact: {
            name: '',
            phone: '',
            email: ''
        }
    };

    constructor(
        private toastyService: ToastyService,
        private route: ActivatedRoute,
        private router: Router,
        private vehicleService: VehicleService) {

        route.params.subscribe(p => {
            if (p['id']) {
                this.vehicle.id = +p['id']; // put + to convert to number
            }
        });
    }

    ngOnInit() {
        var sources = [
            this.vehicleService.getMakes(),
            this.vehicleService.getFeatures()
        ];

        if (this.vehicle.id) {
            sources.push(this.vehicleService.getVehicle(this.vehicle.id));
        }

        // Parallel request (observables)
        Observable.forkJoin(sources).subscribe(data => {
            this.makes = data[0];
            this.features = data[1];

            if (this.vehicle.id) {
                this.setVehicle(data[2]);
                this.populateModels();
            }
        }, err => {
            // This error is for ALL the observables (not only for the vehicle)
            if (err.status == 404) {
                this.router.navigate(['/']);
            }
        });
    }

    onMakeChange() {
        this.populateModels();
        delete this.vehicle.modelId;
    }

    onFeatureToggle(featureId, $event) {
        if ($event.target.checked) {
            this.vehicle.features.push(featureId);
        }
        else {
            var index = this.vehicle.features.indexOf(featureId);
            this.vehicle.features.splice(index, 1);
        }
    }

    submit() {
        var result$ = (this.vehicle.id) ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle);
        result$.subscribe(vehicle => {
            this.toastyService.success({
                title: 'Success',
                msg: 'Data was sucessfully saved.',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });
            this.router.navigate(['/vehicles/', vehicle.id])
        });
    }

    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models = selectedMake.models;
    }

    private setVehicle(v: Vehicle) {
        this.vehicle.id = v.id;
        this.vehicle.makeId = v.make.id;
        this.vehicle.modelId = v.model.id;
        this.vehicle.isRegistered = v.isRegistered;
        this.vehicle.contact = v.contact,
            this.vehicle.features = _.pluck(v.features, 'id');
    }
}
