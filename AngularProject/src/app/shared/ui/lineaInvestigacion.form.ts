import { Component, EventEmitter, input, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { TranslocoModule, TranslocoService } from '@jsverse/transloco';
import { AuthService } from '@services/auth.service';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { InputMaskModule } from 'primeng/inputmask';
import { StyleClassModule } from 'primeng/styleclass';
import { RippleModule } from 'primeng/ripple';
import { catchError, filter, take, throwError } from 'rxjs';
import { InputNumberModule } from 'primeng/inputnumber';
import { FileUploadEvent, FileUploadModule, UploadEvent } from 'primeng/fileupload';
import { AutoCompleteCompleteEvent, AutoCompleteModule, AutoCompleteSelectEvent } from 'primeng/autocomplete';
import { UsuarioService } from '@services/usuario.service';
import { DropdownModule } from 'primeng/dropdown';
import { CommonModule } from '@angular/common';
import { IUserInfo } from '@domain/user.model';
import { AccountState } from '@state/account.state';
import { FormComponent } from './form.component';
import { LineaInvestigacionService } from '@services/lineaInvestigacion.service';
import { ILineaInvestigacionInfo } from '@domain/lineaInvestigacion.model';
import { IFacultadInfo } from '@domain/facultad.model';
import { FacultadState } from '@state/facultad.state';
import { FacultadService } from '@services/facultad.service';
import { GrupoInvestigacionService } from '@services/grupoInvestigacion.service';
import { IGrupoInvestigacionInfo } from '@domain/grupoInvestigacion.model';
import { GrupoInvestigacionState } from '@state/grupoInvestigacion.state';

@Component({
    selector: 'lineaInvestigacion-form',
    templateUrl: './lineaInvestigacion.form.html',
    standalone: true,
    imports: [
        CommonModule,
        TranslocoModule,
        ReactiveFormsModule,
        RouterModule,
        CheckboxModule,
        InputTextModule,
        InputNumberModule,
        DropdownModule,
        AutoCompleteModule,
        StyleClassModule,
        InputMaskModule,
        ButtonModule,
        FileUploadModule,
        RippleModule
    ]
})

export class LineaInvestigacionForm extends FormComponent<ILineaInvestigacionInfo> implements OnInit {
    lineaInvestigacionService = inject(LineaInvestigacionService)
    grupoInvestigacionService = inject(GrupoInvestigacionService);
    facultadService = inject(FacultadService);
    grupoInvestigacionState = inject(GrupoInvestigacionState);
    facultadState = inject(FacultadState);
    filteredGruposInvestigacion: IGrupoInvestigacionInfo[] = [];
    filteredFacultades: IFacultadInfo[] = [];

    submitForm() {
        console.log(this.formGroup.value);
        switch (this.formMode) {
            case 0:
                if (!this.formGroup.valid) return;
                this.lineaInvestigacionService.add(this.formGroup.value)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result); 
                    },
                });
                break;
            case 1:
                if (!this.formGroup.valid) return;
                this.lineaInvestigacionService.update(this.formGroup.value)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result);
                    },
                });            
                break;
            case 2:
                if (this.selectedObject?.id==null) return;
                this.lineaInvestigacionService.delete(this.selectedObject?.id!)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result);
                    },
                });            
                break;
        }
    }

    onInit() {
        this.formGroup.addControl('nombre', new FormControl('', [Validators.required]));
        this.formGroup.addControl('facultadId', new FormControl('', [Validators.required]));
        this.formGroup.addControl('grupoInvestigacionId', new FormControl('', [Validators.required]));

        this.grupoInvestigacionState.stateItem$.pipe(
            filter(x => x!=null),
            take(1)
        )
        .subscribe(result => {
            if (result) {
                this.filteredGruposInvestigacion = result;
            }
        });

        this.facultadState.stateItem$.pipe(
            filter(x => x!=null),
            take(1)
        )
        .subscribe(result => {
            if (result) {
                this.filteredFacultades = result;
            }
        });        
    }
}
