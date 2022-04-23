const regions = {
    BasStLaurent: 'Bas St-Laurent',
    Gaspesie: 'Gaspésie',
    CapitaleNationale: 'Capitale-Nationale',
    Monteregie: 'Montérégie',
    Estrie: 'Estrie',
    Saguenay: 'Saguenay',
    NordDuQuebec: 'Nord-du-Québec',
    ChaudiereAppalaches: 'Chaudière-Appalaches',
    Mauricie: 'Mauricie',
    CentreDuQuebec: 'Centre-du-Québec',
    IleMontreal: 'Île de Montréal',
    Laval: 'Laval',
    Lanaudiere: 'Lanaudière',
    Laurentides: 'Laurentides',
    Outaouais: 'Outaouais',
    Abitibi: 'Abiti-Témiscamingue',
    CoteNord: 'Côte-Nord'  
};

export function formaterRegion(region) {
    const nomRegion = regions[region];
    return nomRegion ? nomRegion : 'Aucune';
}