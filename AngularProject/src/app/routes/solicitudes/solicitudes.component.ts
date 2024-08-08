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
import { ISolicitudInfo, SolicitudStatus } from '@domain/solicitud.model';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { InputMaskModule } from 'primeng/inputmask';
import { StyleClassModule } from 'primeng/styleclass';
import { SolicitudForm } from '@ui/solicitud.form';
import { SolicitudService } from '@services/solicitud.service';
import { SolicitudState } from '@state/solicitud.state';
import { TranslocoLocaleModule } from '@jsverse/transloco-locale';
import { AccountState } from '@state/account.state';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-solicitudes',
  templateUrl: './solicitudes.component.html',
  styleUrl: './solicitudes.component.css',
  standalone: true,
  imports: [
    TranslocoModule,
    TranslocoLocaleModule,
    ButtonModule,
    ReactiveFormsModule,
    SolicitudForm,
    DialogModule,
    NgFor,
    RippleModule,
    InputTextModule,
    TableModule,
    AutoCompleteModule,
    StyleClassModule,
    InputMaskModule,
]
})
export class SolicitudesComponent implements OnInit {
    http = inject(HttpClient);
    solicitudState = inject(SolicitudState);
    solicitudService = inject(SolicitudService);
    translocoService = inject(TranslocoService);
    accountState = inject(AccountState);
    public selectedSolicitud: any | undefined;
    public solicitudes: ISolicitudInfo[] = [];
    formularios: any = {};
    cols: any[] = [];
    rol: UserRole = UserRole.User;

    updateSolicitud(aceptado: boolean) {
        this.solicitudService.update({
            "id": this.selectedSolicitud.id,
            "estado": aceptado ? SolicitudStatus.Aceptado : SolicitudStatus.Rechazado
        }).subscribe();
    }

    ngOnInit() {
        this.accountState.stateItem$.pipe(
            filter(x => x!=null && x.role!=null),
            take(1)
        ).subscribe(accountInfo => {
            this.solicitudState.stateItem$.subscribe(solicitudes => {
                if (solicitudes) {
                    this.solicitudes = [];
                    solicitudes.forEach(solicitud => {
                        if (solicitud.asesorUserId==accountInfo?.id || solicitud.asesoradoUserId==accountInfo?.id) {
                            this.solicitudes.push(solicitud);
                        }
                    });
                }
            })
        })

        // <ng-template pTemplate="header">
        //     <tr>
        //         <th>{{ t('etiquetas.id') }}</th>
        //         <th>{{ t('etiquetas.fechaSolicitud') }}</th>
        //         <th>{{ t('etiquetas.estado') }}</th>
        //         <th>{{ t('etiquetas.nombreDel', {valor: t('roles.3.singular')}) }}</th>
        //         <th>{{ t('etiquetas.nombreDel', {valor: t('roles.2.singular')}) }}</th>                
        //     </tr>
        // </ng-template>
        // <ng-template pTemplate="body" let-solicitud>
        //     <tr [pSelectableRow]="solicitud">
        //         <td>{{ solicitud.id }}</td>
        //         <td>{{ solicitud.fechaCreacion | translocoDate: { dateStyle: 'medium', timeStyle: 'medium' } }}</td>
        //         <td>{{ t('solicitudStatus.'+solicitud.estado) }}</td>
        //         <td>{{ solicitud.asesorado ? solicitud.asesorado.fullName : "" }}</td>
        //         <td>{{ solicitud.asesor ? solicitud.asesor.fullName : "" }}</td>
        //     </tr>
        // </ng-template>

        this.translocoService.selectTranslation().subscribe(labels => {
            this.cols[0] = { date: 'fechaCreacion',
                header: labels['etiquetas.fechaCreacion']};
            this.cols[1] = { date: 'fechaRespuesta',
                header: labels['etiquetas.fechaRespuesta']};
            this.cols[2] = { estado: 'estado',
                header: labels['etiquetas.estado']};
            this.accountState.stateItem$.pipe(
                filter(x => x!=null && x.role!=null),
                take(1)
            ).subscribe(accountInfo => {
                this.rol = accountInfo?.role!;
                if (this.rol == UserRole.Asesor) {
                    this.cols[3] = { field1: 'asesorado', field2: 'fullName',
                        header: this.translocoService.translate('etiquetas.nombreDel', {valor: labels['roles.3.singular']})};
                } else if (this.rol == UserRole.Asesorado){
                    this.cols[3] = { field1: 'asesor', field2: 'fullName',
                        header: this.translocoService.translate('etiquetas.nombreDel', {valor: labels['roles.2.singular']})};
                }
            })
        });
    }
}