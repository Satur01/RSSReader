# MVVM VI El ViewModel
Hoy nos toca ver el último de los tres componentes de [**MVVM**](https://saturninopimentel.com/tag/mvvm/) que hemos visto a lo largo de esta serie de post el **ViewModel**.
El **ViewModel** es el encargado de conectar a los modelos con la vista, de controlar la lógica de la aplicación y de manejar el flujo de navegación, como mencionamos en la analogía en el post [anterior](https://saturninopimentel.com/mvvm-v-el-modelo/) los **ViewModels** son todos los engranes y sistemas que permiten al conductor (usuario) dirigir al automóvil hacia donde el desea, es en los **ViewModels** donde concentraremos las propiedades que servirán para hacer el atado de datos con la **vista**, las implementaciones de [**ICommand**](https://saturninopimentel.com/mvvm-vii-icommand-y-delegatecommand/) para vincular funcionalidad y aquí llamaremos a los **modelos** para trabajar con los datos, por lo regular cuando trabajamos con un buen enfoque de **MVVM** los modelos terminan sirviendo para diversos **ViewModels** y un **ViewModel** van ligado a una vista, aunque pueden ser utilizadas por más de una de ellas.

Sigamos con el ejemplo del post anterior en el cual ya hemos creado nuestras clases del modelo, en esta ocasión agregaremos el **ViewModel**.

***Nota:** antes de continuar te invito a que leas el post de la serie [MVVM VII ICommand y DelegateCommand](https://saturninopimentel.com/mvvm-vii-icommand-y-delegatecommand/) para que puedas hacer uso de los comandos.*

```language-csharp

 public class VMMain : BindableBase
    {

        public VMMain()
        {
            mainModel = new RSSModel();
            mainModel.OnRssDownloadCompleted += OnRssDownloadCompleted;
            _getRssCommand = new Lazy<DelegateCommand>(() => new DelegateCommand(GetRssCommandExecute));
        }

        #region Models

        private RSSModel mainModel;

        #endregion

        #region Commands

        private Lazy<DelegateCommand> _getRssCommand;

        public ICommand GetRssCommand
        {
            get { return _getRssCommand.Value; }
        }

        #endregion

        #region Fields

        private ObservableCollection<rssChannelItem> _items;

        #endregion

        #region Properties

        public ObservableCollection<rssChannelItem> Items
        {
            get { return _items ?? (_items = new ObservableCollection<rssChannelItem>()); }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        private async void GetRssCommandExecute()
        {
            await mainModel.RssDownload();
        }

        #endregion

        #region Event handler

        private void OnRssDownloadCompleted(object sender, OnRssDownloadEventArgs e)
        {
            Items = new ObservableCollection<rssChannelItem>(e.Result.channel.item);
        }

        #endregion

    }
```
En el código anterior hemos creado la instancia de nuestro modelo (RSSModel), el método que nos servirá para controlar el evento que se dispara una vez obtenemos respuesta de nuestro servicio de RSS (OnRssDownloadCompleted), además de un comando que nos servirá para solicitar la consulta del servicio a través de nuestro modelo (GetRssCommand) y por último la propiedad que nos servirá para mostrar los datos en la vista en esta caso **ObservableCollection< rssChannelItem > Items**. 
Para vincular nuestra vista con nuestro **ViewModel** podemos iniciar por crear una instancia, esta instancia la vamos a crear en la vista, para lograrlo vamos a agregar el espacio de nombres en la página como se muestra continuación.
```language-xaml
xmlns:vm="using:RSSReader.ViewModels"
```
Después crearemos una instancia y la vincularemos al **DataContext** de la página.
```language-xaml
<Page.DataContext>
        <vm:VMMain />
</Page.DataContext>
```
Con esto tendremos nuestra **ViewModel** lista para trabajar en conjunto con nuestra vista.

####VMLocator
El código mostrado anteriormente nos sirve, pero tiene un desventaja que es el hecho de que cada vez que realicemos un cambio en el **ViewModel** vinculado a las vistas tendremos que buscar los cambios en todas las vistas y esto podría generar errores, para evitar estos problemas podemos hacer uso de un concepto llamado **locator** el trabajo de este **locator** consiste en agrupar todos los **ViewModels** y exponerlos a través de propiedades que pueden ser utilizadas para asignar a los contextos de las vistas, con lo cual reduciremos el trabajo cuando existan modificaciones y tendremos un solo lugar donde podremos crear las instancias de nuestros **ViewModels**.

Recomiendo que al utilizar este **locator** utilices **Lazy< T >** para optimizar la creación de las instancias de los **ViewModels**, ya que solo se creará una instancia cuando llames a la clase por primera vez.

Para hacer uso de **VMLocator** vamos a crear una clase donde por medio de propiedades vamos a exponer los **ViewModels** tal como se muestra en el siguiente ejemplo.

```language-csharp
public class VMLocator
{
    public VMLocator()
    {
        _vMMain = new Lazy<VMMain>(() => new VMMain());
    }

    private Lazy<VMMain> _vMMain;

    public VMMain VMMain
    {
        get { return _vMMain.Value; }
    }
}
```
Después de crear esta clase vamos a agregar un recurso en el archivo App.xaml, empecemos por agregar el espacio de nombres .
```language-chsarp
xmlns:vmBase="using:RSSReader.ViewModels.ViewModelBase"
```
Y después crearemos un recurso para utilizar nuestro **VMlocator**.
```language-xaml
<Application.Resources>
        <ResourceDictionary>
            <vmBase:VMLocator x:Key="VmLocator" />
        </ResourceDictionary>
</Application.Resources>
```
Una vez que completemos estos sencillos pasos podemos hacer los cambios en nuestra vista para apuntar a la propiedad de nuestro recurso tal como se muestra a continuación.
```language-csharp
DataContext="{Binding VMMain,
                            Source={StaticResource VmLocator}}"
```
Como podrás notar esto no afecta el funcionamiento ya que solo cambiamos la forma en que obtenemos nuestra instancia del **ViewModel**.

Con esto terminamos la primera parte de la serie de **MVVM**, a partir de este punto ya puedes crear una implementación de **MVVM** con las mejores practicas, debes tener en cuenta que este es el enfoque general de **MVVM** y que existen otros, para muestra puedes ver las implementaciones de los diferentes frameworks de **MVVM**, puedes crear tu propia implementación también, en lo personal procuro no utilizo ningún framework ya que me gusta crear el código y ver los escenarios posibles, pero existen situaciones en las que el volumen de trabajo lo exige, te invito a formarte una opinión de cada uno de los frameworks.

Te dejo el código del ejemplo del lector de feeds [aquí](https://github.com/Satur01/RSSReader) para que puedas revisarlo, espero te resulte de utilidad esta serie de post, espero tus comentarios y opiniones.

Saludos!!
