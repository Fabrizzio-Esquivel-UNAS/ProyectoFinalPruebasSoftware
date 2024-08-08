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
import { IEscuelaInfo } from '@domain/escuela.model';
import { UsuarioService } from '@services/usuario.service';
import { DropdownModule } from 'primeng/dropdown';
import { CommonModule } from '@angular/common';
import { IUserInfo, UserRole } from '@domain/user.model';
import { AccountState } from '@state/account.state';
import { FormComponent } from './form.component';
import { EscuelaState } from '@state/escuela.state';
import { EscuelaService } from '@services/escuela.service';
import { LineaInvestigacionService } from '@services/lineaInvestigacion.service';
import { LineaInvestigacionState } from '@state/lineaInvestigacion.state';
import { ILineaInvestigacionInfo } from '@domain/lineaInvestigacion.model';

@Component({
    selector: 'user-form',
    templateUrl: './user.form.html',
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

export class UserForm extends FormComponent<IUserInfo> implements OnInit {
    usuarioService = inject(UsuarioService);
    lineaInvestigacionService = inject(LineaInvestigacionService);
    lineaInvestigacionState = inject(LineaInvestigacionState);
    escuelaService = inject(EscuelaService);
    escuelaState = inject(EscuelaState)
    router = inject(Router);
    rol = UserRole.User;
    userId: string = "";
    roles: any[] = [];

    filteredEscuelas: IEscuelaInfo[] = [];
    filteredLineas: ILineaInvestigacionInfo[] = [];

    submitForm() {
        console.log(this.formGroup.value);
        switch (this.formMode) {
            case 0:
                if (!this.formGroup.valid) return;
                this.usuarioService.add(this.formGroup.value)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result); 
                    },
                });
                break;
            case 1:
                if (!this.formGroup.valid) return;
                this.usuarioService.update(this.formGroup.value)
                .subscribe({
                    next: (_result) => {
                        this.visibleChange.emit(false);
                        this.submitEvent.emit(_result);
                    },
                });            
                break;
            case 2:
                if (this.selectedObject?.id==null) return;
                this.usuarioService.delete(this.selectedObject?.id!)
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
        this.formGroup.addControl('firstName', new FormControl('', [Validators.required]));
        this.formGroup.addControl('lastName', new FormControl('', [Validators.required]));
        this.formGroup.addControl('escuelaId', new FormControl('', [Validators.required]));
        this.formGroup.addControl('lineaInvestigacionId', new FormControl('', [Validators.required]));
        this.formGroup.addControl('codigo', new FormControl('', [Validators.required]));
        this.formGroup.addControl('telefono', new FormControl('', [Validators.required]));
        this.formGroup.addControl('email', new FormControl('', [Validators.required, Validators.email]));
        if (this.formMode==this.formModes.add){
            this.formGroup.addControl('password', new FormControl('', [Validators.required]));
        }
        this.formGroup.addControl('foto', new FormControl('', []));
        this.formGroup.addControl('role', new FormControl('', [Validators.required]));
        
        this.escuelaState.stateItem$.pipe(
            filter(x => x!=null),
            take(1)
        ).subscribe(escuelas => {
            if (escuelas) {
                this.filteredEscuelas = escuelas;
            }
        })
        this.lineaInvestigacionState.stateItem$.pipe(
            filter(x => x!=null),
            take(1)
        ).subscribe(lineas => {
            if (lineas) {
                this.filteredLineas = lineas;
            }
        })
        
        this.accountState.stateItem$.pipe(
            filter(x => x!=null && x.role!=null),
            take(1)
        ).subscribe(accountInfo => {
            this.rol = accountInfo?.role!;
            this.userId = accountInfo?.id!;
            if (this.rol==UserRole.Coordinador){
                this.formGroup.controls['role'].setValue(UserRole.Asesor);
            }

            this.translocoService.selectTranslation().subscribe(itemLabels => {
                this.roles = [];
                const stringKeys = Object
                    .keys(UserRole)
                    .filter((v) => isNaN(Number(v)))
        
                stringKeys.forEach((key, index) => {
                    if (!(index==UserRole.User || index==UserRole.Asesor) && this.rol==UserRole.Coordinador) {
                        return;
                    }
                    this.roles.push({ label: itemLabels['roles.'+index+'.singular'], value: index});
                })        
            })
        })
    }

    onUpload(data: FileUploadEvent) {
    }
}
