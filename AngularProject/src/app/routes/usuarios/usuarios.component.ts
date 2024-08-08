import { Component, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, filter, map, take, tap } from 'rxjs/operators';
import { TableModule, TableRowSelectEvent, TableRowUnSelectEvent } from 'primeng/table';
import { TranslocoModule } from '@jsverse/transloco';
import { IUserInfo, UserRole } from '@domain/user.model';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { UsuarioService } from '@services/usuario.service';
import { IEscuelaInfo } from '@domain/escuela.model';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { InputMaskModule } from 'primeng/inputmask';
import { StyleClassModule } from 'primeng/styleclass';
import { UserForm } from '@ui/user.form';
import { UsuarioState } from '@state/usuario.state';
import { AccountService } from '@services/account.service';
import { AccountState } from '@state/account.state';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrl: './usuarios.component.css',
  standalone: true,
  imports: [
    TranslocoModule,
    ButtonModule,
    ReactiveFormsModule,
    UserForm,
    DialogModule,
    RippleModule,
    InputTextModule,
    TableModule,
    AutoCompleteModule,
    StyleClassModule,
    InputMaskModule,
]
})
export class UsuariosComponent implements OnInit {
    http = inject(HttpClient);
    usuarioState = inject(UsuarioState);
    accountState = inject(AccountState);
    selectedUsuario: IUserInfo | undefined;
    usuarios: IUserInfo[] = [];
    formularios: any = {};
    escuelas: IEscuelaInfo[] = [];
    filteredEscuelas: IEscuelaInfo[] = [];
    rol: UserRole = UserRole.User;

    public editarForm = new FormGroup({
        firstName: new FormControl('', [Validators.required]),
        lastName: new FormControl('', [Validators.required]),
        escuelaId: new FormControl('', [Validators.required]),
        codigo: new FormControl('', [Validators.required]),
        telefono: new FormControl('', [Validators.required]),
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required]),
    })

    public onSelect() {

    }

    public onSubmit() {
        if (!this.editarForm.valid) return
    }

    ngOnInit() {
        this.accountState.stateItem$.subscribe(account => {
            if (account!=null && account.role!=null){
                this.rol = account.role;
            }
        })
        this.accountState.stateItem$.pipe(
            filter(x => x!=null && x.role!=null),
            take(1)
        ).subscribe(accountInfo => {
            this.usuarioState.stateItem$.subscribe(usuarioes => {
                if (usuarioes) {
                    this.usuarios = [];
                    usuarioes.forEach(usuario => {
                        if (usuario.id!=accountInfo?.id) {
                            if (accountInfo?.role==UserRole.Coordinador && !(usuario.role==UserRole.User || usuario.role==UserRole.Asesor)){
                                return;
                            }
                            this.usuarios.push(usuario);
                        }
                    });
                }
            })
        })
    }
}
