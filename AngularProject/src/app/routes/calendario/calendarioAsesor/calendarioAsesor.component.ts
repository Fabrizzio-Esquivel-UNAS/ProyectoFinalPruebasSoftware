import { Component, Inject, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { RippleModule } from 'primeng/ripple';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TranslocoModule } from '@jsverse/transloco';
import { UsuarioService } from '@services/usuario.service';
import { reduce, tap } from 'rxjs';
import { AccountState } from '@state/account.state';
import { PrimeNGConfig } from 'primeng/api';
import { SolicitudService } from '@services/solicitud.service';
import { CalendarioService } from '@services/calendario.service';
import { CalendarioState } from '@state/calendario.state';
import { DialogModule } from 'primeng/dialog';

@Component({
    selector: 'app-calendarioasesor',
    templateUrl: './calendarioasesor.component.html',
    standalone: true,
    imports: [
        TranslocoModule,
        AutoCompleteModule,
        ReactiveFormsModule,
        InputTextareaModule,
        InputTextModule,
        InputNumberModule,
        DialogModule,
        ButtonModule,
        RippleModule,
        FormsModule
    ]
})

export class CalendarioAsesorComponent implements OnInit {
    calendarioService = inject(CalendarioService);
    calendarioState = inject(CalendarioState);
    calendarioId: string | undefined;
    calendarioUrl: string | undefined;
    visible: any = {};
    nombreEvento = "";

    onAutorizar() {
        this.calendarioService.auth();
    }
    onVincular() {
        this.calendarioService.link(this.calendarioId!, this.nombreEvento).subscribe(
            result => {
        });
    }
    onEliminar() {
        this.calendarioService.delete(this.calendarioId!).subscribe();
    }
    redirectCalendario(){
        if (this.calendarioUrl) {
            document.location.href = this.calendarioUrl;        
        }
    }
    ngOnInit() {
        this.calendarioState.stateItem$.subscribe({
            next: (res) => {
                if (res) {
                    console.log(res);
                    this.calendarioId = res.id!;
                    this.calendarioUrl = res.schedulingUrl;
                }
            },
            error: (err) => {
                console.log(err);
                this.calendarioId = undefined;
                this.calendarioUrl = undefined;
            }

        })
    }
}
