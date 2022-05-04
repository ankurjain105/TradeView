import { CommodityModel } from "src/core/models/commoditymodel.model";
import { KeyValuePair } from "src/core/models/keyvaluepair.model";

export interface Pnl {
    date: Date;
    dailyPnl?: number | null;
}

export interface PnlSummary {
    model?: CommodityModel | null;
    pnls?: Pnl[] | null;
}

export interface PnlHistory {
    pnlSummaries: PnlSummary[];
}