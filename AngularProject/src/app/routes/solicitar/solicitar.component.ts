import { Component, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AutoCompleteCompleteEvent, AutoCompleteModule, AutoCompleteSelectEvent } from 'primeng/autocomplete';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { RippleModule } from 'primeng/ripple';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TranslocoModule } from '@jsverse/transloco';
import { UsuarioService } from '@services/usuario.service';
import { tap } from 'rxjs';
import { AccountState } from '@state/account.state';
import { PrimeNGConfig } from 'primeng/api';
import { SolicitudService } from '@services/solicitud.service';
import { ILineaInvestigacionInfo } from '@domain/lineaInvestigacion.model';
import { LineaInvestigacionService } from '@services/lineaInvestigacion.service';
import { IUserInfo } from '@domain/user.model';

@Component({
    selector: 'app-solicitar',
    templateUrl: './solicitar.component.html',
    standalone: true,
    imports: [
        TranslocoModule,
        AutoCompleteModule,
        ReactiveFormsModule,
        InputTextareaModule,
        InputTextModule,
        InputNumberModule,
        ButtonModule,
        RippleModule,
        FormsModule
    ]
})

export class SolicitarComponent implements OnInit {
    primengConfig = inject(PrimeNGConfig);
    usuarioService = inject(UsuarioService);
    lineasInvestigacionService = inject(LineaInvestigacionService);
    solicitudService = inject(SolicitudService);
    accountState = inject(AccountState);
    asesores: any[] = [];
    filteredAsesores: any[] = [];
    filteredLineas: any[] = [];
    asesorLineaId: string | undefined;

    protected solicitudForm = new FormGroup({
        lineaInvestigacionId: new FormControl('', [Validators.required]),
        asesorUserId: new FormControl('', [Validators.required]),
        asesoradoUserId: new FormControl('', [Validators.required]),
        numeroTesis: new FormControl('', [Validators.required]),
        mensaje: new FormControl('', [Validators.required])
    })

    onSelectLinea(event : AutoCompleteSelectEvent) {
        if (event.value.id!=this.asesorLineaId){
            this.solicitudForm.controls.asesorUserId.setValue("");
        }
    }
    onSelectAsesor(event : AutoCompleteSelectEvent) {
        this.asesorLineaId = event.value.lineaInvestigacionId!;
        if (event.value.lineaInvestigacionId!=this.solicitudForm.controls.lineaInvestigacionId.value){
            this.solicitudForm.controls.lineaInvestigacionId.setValue("");
        }
    }

    onSubmit() {
        var currentUserId = this.accountState.getState()?.id!;
        this.solicitudForm.controls.asesoradoUserId.setValue(currentUserId);
        this.solicitudService.add(this.solicitudForm.value).subscribe();
    }

    ngOnInit() {
    }
}
