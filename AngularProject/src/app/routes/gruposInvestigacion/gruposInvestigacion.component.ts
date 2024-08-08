import { Component, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { TableModule, TableRowSelectEvent, TableRowUnSelectEvent } from 'primeng/table';
import { TranslocoModule } from '@jsverse/transloco';
import { IUserInfo } from '@domain/user.model';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { UsuarioService } from '@services/usuario.service';
import { IGrupoInvestigacionInfo } from '@domain/grupoInvestigacion.model';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { InputMaskModule } from 'primeng/inputmask';
import { StyleClassModule } from 'primeng/styleclass';
import { GrupoInvestigacionForm } from '@ui/grupoInvestigacion.form';
import { GrupoInvestigacionService } from '@services/grupoInvestigacion.service';
import { GrupoInvestigacionState } from '@state/grupoInvestigacion.state';

@Component({
  selector: 'app-gruposinvestigacion',
  templateUrl: './gruposinvestigacion.component.html',
  styleUrl: './gruposinvestigacion.component.css',
  standalone: true,
  imports: [
    TranslocoModule,
    ButtonModule,
    ReactiveFormsModule,
    GrupoInvestigacionForm,
    DialogModule,
    RippleModule,
    InputTextModule,
    TableModule,
    AutoCompleteModule,
    StyleClassModule,
    InputMaskModule,
]
})
export class GruposInvestigacionComponent implements OnInit {
    http = inject(HttpClient);
    grupoinvestigacionState = inject(GrupoInvestigacionState);
    public selectedGrupoInvestigacion: any | undefined;
    public gruposInvestigacion: IGrupoInvestigacionInfo[] = [];
    formularios: any = {};

    ngOnInit() {
        this.grupoinvestigacionState.stateItem$.subscribe(gruposInvestigacion => {
            if (gruposInvestigacion) {
                this.gruposInvestigacion = gruposInvestigacion;
            }
        })
    }
}