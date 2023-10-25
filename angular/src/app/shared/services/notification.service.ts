import { Injectable } from "@angular/core"; 
import { MessageService } from "primeng/api";
@Injectable()
export class NotificationService {
    constructor(private messageService: MessageService){

    }

    showSucces(message:string){
        this.messageService.add({severity:'success', summary:'Thành công', detail:message})
    }

    showError(message:string){
        this.messageService.add({severity:'err', summary:'Lỗi', detail:message})
    }
}