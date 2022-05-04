import { KeyValuePair } from "./keyvaluepair.model";

export class CommodityModel {
    id: number = 0;
    name: string = '';
    commodity?: KeyValuePair | null = null;
    model?: KeyValuePair | null = null;
}