import { Component, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, filter, map, take, tap } from 'rxjs/operators';
import { TableModule, TableRowSelectEvent, TableRowUnSelectEvent } from 'primeng/table';
import { TranslocoModule, TranslocoService } from '@jsverse/transloco';
import { IUserInfo, UserRole } from '@domain/user.model';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { UsuarioService } from '@services/usuario.service';
import { ICitaInfo } from '@domain/cita.model';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { InputMaskModule } from 'primeng/inputmask';
import { StyleClassModule } from 'primeng/styleclass';
import { CitaForm } from '@ui/cita.form';
import { CitaService } from '@services/cita.service';
import { CitaState } from '@state/cita.state';
import { CalendarioService } from '@services/calendario.service';
import { CalendarioState } from '@state/calendario.state';
import { TranslocoLocaleModule } from '@jsverse/transloco-locale';
import { NgFor } from '@angular/common';
import { AccountState } from '@state/account.state';

@Component({
  selector: 'app-citas',
  templateUrl: './citas.component.html',
  styleUrl: './citas.component.css',
  standalone: true,
  imports: [
    TranslocoModule,
    TranslocoLocaleModule,
    ButtonModule,
    NgFor,
    ReactiveFormsModule,
    CitaForm,
    DialogModule,
    RippleModule,
    InputTextModule,
    TableModule,
    AutoCompleteModule,
    StyleClassModule,
    InputMaskModule,
]
})
export class CitasComponent implements OnInit {
    http = inject(HttpClient);
    accountState = inject(AccountState);
    citaState = inject(CitaState);
    citaService = inject(CitaService);
    calendarioService = inject(CalendarioService);
    calendarioState = inject(CalendarioState);
    translocoService = inject(TranslocoService);
    public selectedCita: any | undefined;
    public citas: ICitaInfo[] = [];
    formularios: any = {};
    cols: any[] = []; 

    onRefresh() {
        if (this.calendarioService.calendarioUserId){
            this.calendarioService.sync(this.calendarioState.getState()?.id!).pipe(
                catchError((error) => {
                    return throwError(() => error);
                })
            ).subscribe(res => {
                this.citaService.getAll().subscribe();
            });
        }
    }

    ngOnInit() {
        this.accountState.stateItem$.pipe(
            filter(x => x!=null && x.role!=null),
            take(1)
        ).subscribe(accountInfo => {
            this.citaState.stateItem$.subscribe(citas => {
                if (citas) {
                    this.citas = [];
                    citas.forEach(cita => {
                        if (cita.asesorUserId==accountInfo?.id || cita.asesoradoUserId==accountInfo?.id) {
                            this.citas.push(cita);
                        }
                    });
                }
            })
        })
        
        this.translocoService.selectTranslation().subscribe(labels => {
            this.cols = [];
            this.cols.push({ date: 'fechaCreacion',
                header: labels['etiquetas.fechaCreacion']});
            this.cols.push({ estado: 'estado',
                header: labels['etiquetas.estado']});
            this.cols.push({ date: 'fechaInicio',
                header: labels['etiquetas.fechaInicio']});
            this.cols.push({ date: 'fechaFin',
                header: labels['etiquetas.fechaFin']});

            this.accountState.stateItem$.pipe(
                filter(x => x!=null && x.role!=null),
                take(1)
            ).subscribe(accountInfo => {
                let rol = accountInfo?.role!;
                if (rol == UserRole.Asesor) {
                    this.cols.push({ field1: 'asesorado', field2: 'fullName',
                        header: this.translocoService.translate('etiquetas.nombreDel', {valor: labels['roles.3.singular']})});
                } else if (rol == UserRole.Asesorado){
                    this.cols.push({ field1: 'asesor', field2: 'fullName',
                        header: this.translocoService.translate('etiquetas.nombreDel', {valor: labels['roles.2.singular']})});
                }                
            })
        })
    }
}
