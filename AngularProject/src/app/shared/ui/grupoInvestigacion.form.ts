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
import { IFacultadInfo } from '@domain/facultad.model';
import { FacultadState } from '@state/facultad.state';
import { FacultadService } from '@services/facultad.service';
import { GrupoInvestigacionService } from '@services/grupoInvestigacion.service';
import { IGrupoInvestigacionInfo } from '@domain/grupoInvestigacion.model';
import { UsuarioState } from '@state/usuario.state';

@Component({
    selector: 'grupoInvestigacion-form',
    templateUrl: './grupoInvestigacion.form.html',
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

export class GrupoInvestigacionForm extends FormComponent<IGrupoInvestigacionInfo> implements OnInit {
    grupoInvestigacionService = inject(GrupoInvestigacionService)
    usuarioService = inject(UsuarioService);
    usuarioState = inject(UsuarioState);
    filteredUsuarios: IUserInfo[] = [];

    submitForm() {
        console.log(this.formGroup.value);
        switch (this.formMode) {
            case 0:
                if (!this.formGroup.valid) return;
                this.grupoInvestigacionService.add(this.formGroup.value)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result); 
                    },
                });
                break;
            case 1:
                if (!this.formGroup.valid) return;
                this.grupoInvestigacionService.update(this.formGroup.value)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result);
                    },
                });            
                break;
            case 2:
                if (this.selectedObject?.id==null) return;
                this.grupoInvestigacionService.delete(this.selectedObject?.id!)
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
        this.formGroup.addControl('coordinadorUserId', new FormControl(null, []));

        this.usuarioState.stateItem$.pipe(
            // https://stackoverflow.com/questions/62584424/get-the-first-item-that-matches-a-condition-in-observable
            // https://stackoverflow.com/questions/42345969/take1-vs-first
            filter(x => x!=null),
            take(1)
        )
        .subscribe(result => {
            if (result) {
                this.filteredUsuarios = result;
            }
        });        
    }
}
