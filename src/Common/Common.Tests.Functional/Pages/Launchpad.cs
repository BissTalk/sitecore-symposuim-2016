using System.Collections.Generic;
using Bissol.SymDemo.Common.Tests.Functional.Pages;
using JetBrains.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

public class Launchpad : BasePage<Launchpad>
{
    private readonly string pageLoadedText = "Interactions by visits and value per visits";

    private readonly string pageUrl =
        "/sitecore/client/Applications/Launchpad?sc_lang=en#dateFrom=30-05-2016&dateTo=30-08-2016";

    [FindsBy(How = How.CssSelector, Using = "a[title='Access Viewer']")]
    [CacheLookup]
    private IWebElement accessViewer;

    [FindsBy(How = How.CssSelector, Using = "a[title='App Center']")]
    [CacheLookup]
    private IWebElement appCenter;

    [FindsBy(How = How.CssSelector, Using = "a[title='Campaign Creator']")]
    [CacheLookup]
    private IWebElement
        campaignCreator;

    [FindsBy(How = How.CssSelector, Using = "a[title='Content Editor']")]
    [CacheLookup]
    private IWebElement
        contentEditor;

    [FindsBy(How = How.CssSelector, Using = "a[title='Control Panel']")]
    [CacheLookup]
    private IWebElement controlPanel;

    [FindsBy(How = How.CssSelector,
        Using =
            "#InteractionChartApp div.sc-ExperienceAnalyticsLineChart.sc_ExperienceAnalyticsLineChart_160.data-sc-registered div.sc-ChartComposites div:nth-of-type(2) div.sc-advancedExpander.sc_AdvancedExpander_154.data-sc-registered.open div:nth-of-type(1) table tbody tr:nth-of-type(1) td:nth-of-type(3) div.sc-advancedExpander-header-actionbar-container.data-sc-registered div.sc-actioncontrol.sc-actionpanel.sc_ActionControl_148.data-sc-registered ul.nav li:nth-of-type(2) div.dropdown.data-sc-registered div.sc-foldout.dropdown-menu.open.data-sc-registered div.sc-foldout-content table.sc-actionControl-foldout-grid tbody tr td ul li:nth-of-type(2) a:nth-of-type(1)"
        )]
    [CacheLookup]
    private IWebElement daily1;

    [FindsBy(How = How.CssSelector,
        Using =
            "#CampaignsChartApp div.sc-ExperienceAnalyticsLineChart.sc_ExperienceAnalyticsLineChart_186.data-sc-registered div.sc-ChartComposites div:nth-of-type(2) div.sc-advancedExpander.sc_AdvancedExpander_180.data-sc-registered.open div:nth-of-type(1) table tbody tr:nth-of-type(1) td:nth-of-type(3) div.sc-advancedExpander-header-actionbar-container.data-sc-registered div.sc-actioncontrol.sc-actionpanel.sc_ActionControl_174.data-sc-registered ul.nav li:nth-of-type(2) div.dropdown.data-sc-registered div.sc-foldout.dropdown-menu.open.data-sc-registered div.sc-foldout-content table.sc-actionControl-foldout-grid tbody tr td ul li:nth-of-type(2) a:nth-of-type(1)"
        )]
    [CacheLookup]
    private IWebElement daily2;

    [FindsBy(How = How.CssSelector, Using = "a[title='Desktop']")]
    [CacheLookup]
    private IWebElement desktop;

    [FindsBy(How = How.CssSelector, Using = "a[title='Domain Manager']")]
    [CacheLookup]
    private IWebElement
        domainManager;

    [FindsBy(How = How.CssSelector, Using = "a[title='Experience Analytics']")]
    [CacheLookup]
    private IWebElement
        experienceAnalytics;

    [FindsBy(How = How.CssSelector, Using = "a[title='Experience Editor']")]
    [CacheLookup]
    private IWebElement
        experienceEditor;

    [FindsBy(How = How.CssSelector, Using = "a[title='Experience Optimization']")]
    [CacheLookup]
    private IWebElement
        experienceOptimization;

    [FindsBy(How = How.CssSelector, Using = "a[title='Experience Profile']")]
    [CacheLookup]
    private IWebElement
        experienceProfile;

    [FindsBy(How = How.CssSelector, Using = "a[title='Federated Experience Manager']")]
    [CacheLookup]
    private
        IWebElement federatedExperienceManager;

    [FindsBy(How = How.CssSelector, Using = "a[title='Komfo Social']")]
    [CacheLookup]
    private IWebElement komfoSocial;

    [FindsBy(How = How.CssSelector,
        Using =
            "#InteractionChartApp div.sc-ExperienceAnalyticsLineChart.sc_ExperienceAnalyticsLineChart_160.data-sc-registered div.sc-ChartComposites div:nth-of-type(2) div.sc-advancedExpander.sc_AdvancedExpander_154.data-sc-registered.open div:nth-of-type(2) div:nth-of-type(2) div:nth-of-type(1) a.data-sc-registered"
        )]
    [CacheLookup]
    private IWebElement less1;

    [FindsBy(How = How.CssSelector,
        Using =
            "#CampaignsChartApp div.sc-ExperienceAnalyticsLineChart.sc_ExperienceAnalyticsLineChart_186.data-sc-registered div.sc-ChartComposites div:nth-of-type(2) div.sc-advancedExpander.sc_AdvancedExpander_180.data-sc-registered.open div:nth-of-type(2) div:nth-of-type(2) div:nth-of-type(1) a.data-sc-registered"
        )]
    [CacheLookup]
    private IWebElement less2;

    [FindsBy(How = How.CssSelector, Using = "a[title='List Manager']")]
    [CacheLookup]
    private IWebElement listManager;

    [FindsBy(How = How.CssSelector, Using = "a.logout.data-sc-registered")]
    [CacheLookup]
    private IWebElement logout;

    [FindsBy(How = How.CssSelector, Using = "a[title='Marketing Control Panel']")]
    [CacheLookup]
    private IWebElement
        marketingControlPanel;

    [FindsBy(How = How.CssSelector, Using = "a[title='Media Library']")]
    [CacheLookup]
    private IWebElement mediaLibrary;

    [FindsBy(How = How.CssSelector,
        Using =
            "#InteractionChartApp div.sc-ExperienceAnalyticsLineChart.sc_ExperienceAnalyticsLineChart_160.data-sc-registered div.sc-ChartComposites div:nth-of-type(2) div.sc-advancedExpander.sc_AdvancedExpander_154.data-sc-registered.open div:nth-of-type(1) table tbody tr:nth-of-type(1) td:nth-of-type(3) div.sc-advancedExpander-header-actionbar-container.data-sc-registered div.sc-actioncontrol.sc-actionpanel.sc_ActionControl_148.data-sc-registered ul.nav li:nth-of-type(2) div.dropdown.data-sc-registered div.sc-foldout.dropdown-menu.open.data-sc-registered div.sc-foldout-content table.sc-actionControl-foldout-grid tbody tr td ul li:nth-of-type(4) a:nth-of-type(1)"
        )]
    [CacheLookup]
    private IWebElement monthly1;

    [FindsBy(How = How.CssSelector,
        Using =
            "#CampaignsChartApp div.sc-ExperienceAnalyticsLineChart.sc_ExperienceAnalyticsLineChart_186.data-sc-registered div.sc-ChartComposites div:nth-of-type(2) div.sc-advancedExpander.sc_AdvancedExpander_180.data-sc-registered.open div:nth-of-type(1) table tbody tr:nth-of-type(1) td:nth-of-type(3) div.sc-advancedExpander-header-actionbar-container.data-sc-registered div.sc-actioncontrol.sc-actionpanel.sc_ActionControl_174.data-sc-registered ul.nav li:nth-of-type(2) div.dropdown.data-sc-registered div.sc-foldout.dropdown-menu.open.data-sc-registered div.sc-foldout-content table.sc-actionControl-foldout-grid tbody tr td ul li:nth-of-type(4) a:nth-of-type(1)"
        )]
    [CacheLookup]
    private IWebElement monthly2;

    [FindsBy(How = How.CssSelector,
        Using =
            "#InteractionChartApp div.sc-ExperienceAnalyticsLineChart.sc_ExperienceAnalyticsLineChart_160.data-sc-registered div.sc-ChartComposites div:nth-of-type(2) div.sc-advancedExpander.sc_AdvancedExpander_154.data-sc-registered.open div:nth-of-type(2) div:nth-of-type(2) div:nth-of-type(2) a.data-sc-registered"
        )]
    [CacheLookup]
    private IWebElement more1;

    [FindsBy(How = How.CssSelector,
        Using =
            "#CampaignsChartApp div.sc-ExperienceAnalyticsLineChart.sc_ExperienceAnalyticsLineChart_186.data-sc-registered div.sc-ChartComposites div:nth-of-type(2) div.sc-advancedExpander.sc_AdvancedExpander_180.data-sc-registered.open div:nth-of-type(2) div:nth-of-type(2) div:nth-of-type(2) a.data-sc-registered"
        )]
    [CacheLookup]
    private IWebElement more2;

    [FindsBy(How = How.CssSelector, Using = "a[title='Path Analyzer']")]
    [CacheLookup]
    private IWebElement pathAnalyzer;

    [FindsBy(How = How.CssSelector, Using = "a[title='Recycle Bin']")]
    [CacheLookup]
    private IWebElement recycleBin;

    [FindsBy(How = How.CssSelector, Using = "a[title='Role Manager']")]
    [CacheLookup]
    private IWebElement roleManager;

    [FindsBy(How = How.CssSelector, Using = "a[title='Security Editor']")]
    [CacheLookup]
    private IWebElement
        securityEditor;

    [FindsBy(How = How.CssSelector, Using = "a[title='User Manager']")]
    [CacheLookup]
    private IWebElement userManager;

    [FindsBy(How = How.CssSelector,
        Using =
            "#InteractionChartApp div.sc-ExperienceAnalyticsLineChart.sc_ExperienceAnalyticsLineChart_160.data-sc-registered div.sc-ChartComposites div:nth-of-type(2) div.sc-advancedExpander.sc_AdvancedExpander_154.data-sc-registered.open div:nth-of-type(1) table tbody tr:nth-of-type(1) td:nth-of-type(3) div.sc-advancedExpander-header-actionbar-container.data-sc-registered div.sc-actioncontrol.sc-actionpanel.sc_ActionControl_148.data-sc-registered ul.nav li:nth-of-type(2) div.dropdown.data-sc-registered div.sc-foldout.dropdown-menu.open.data-sc-registered div.sc-foldout-content table.sc-actionControl-foldout-grid tbody tr td ul li:nth-of-type(3) a:nth-of-type(1)"
        )]
    [CacheLookup]
    private IWebElement weekly1;

    [FindsBy(How = How.CssSelector,
        Using =
            "#CampaignsChartApp div.sc-ExperienceAnalyticsLineChart.sc_ExperienceAnalyticsLineChart_186.data-sc-registered div.sc-ChartComposites div:nth-of-type(2) div.sc-advancedExpander.sc_AdvancedExpander_180.data-sc-registered.open div:nth-of-type(1) table tbody tr:nth-of-type(1) td:nth-of-type(3) div.sc-advancedExpander-header-actionbar-container.data-sc-registered div.sc-actioncontrol.sc-actionpanel.sc_ActionControl_174.data-sc-registered ul.nav li:nth-of-type(2) div.dropdown.data-sc-registered div.sc-foldout.dropdown-menu.open.data-sc-registered div.sc-foldout-content table.sc-actionControl-foldout-grid tbody tr td ul li:nth-of-type(3) a:nth-of-type(1)"
        )]
    [CacheLookup]
    private IWebElement weekly2;

    [FindsBy(How = How.CssSelector, Using = "a[title='Workbox']")]
    [CacheLookup]
    private IWebElement workbox;

    [UsedImplicitly]
    public Launchpad([NotNull] IWebDriver driver)
        : base(driver, new Dictionary<string, string>(), 15)
    {
    }

    /// <summary>
    ///     Gets the relative URL of the page.
    /// </summary>
    /// <value>
    ///     The relative URL path.
    /// </value>
    public override string RelativePath => "/sitecore/client/Applications/Launchpad";

    /// <summary>
    ///     Gets the page verification text.
    /// </summary>
    /// <value>
    ///     The page verification text.
    /// </value>
    protected override string PageVerificationText => "class=\"sc-launchpad\"";

    /// <summary>
    ///     Click on Access Viewer Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickAccessViewerLink()
    {
        accessViewer.Click();
        return this;
    }

    /// <summary>
    ///     Click on App Center Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickAppCenterLink()
    {
        appCenter.Click();
        return this;
    }

    /// <summary>
    ///     Click on Campaign Creator Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickCampaignCreatorLink()
    {
        campaignCreator.Click();
        return this;
    }

    /// <summary>
    ///     Click on Content Editor Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickContentEditorLink()
    {
        contentEditor.Click();
        return this;
    }

    /// <summary>
    ///     Click on Control Panel Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickControlPanelLink()
    {
        controlPanel.Click();
        return this;
    }

    /// <summary>
    ///     Click on Daily Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickDaily1Link()
    {
        daily1.Click();
        return this;
    }

    /// <summary>
    ///     Click on Daily Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickDaily2Link()
    {
        daily2.Click();
        return this;
    }

    /// <summary>
    ///     Click on Desktop Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickDesktopLink()
    {
        desktop.Click();
        return this;
    }

    /// <summary>
    ///     Click on Domain Manager Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickDomainManagerLink()
    {
        domainManager.Click();
        return this;
    }

    /// <summary>
    ///     Click on Experience Analytics Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickExperienceAnalyticsLink()
    {
        experienceAnalytics.Click();
        return this;
    }

    /// <summary>
    ///     Click on Experience Editor Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickExperienceEditorLink()
    {
        experienceEditor.Click();
        return this;
    }

    /// <summary>
    ///     Click on Experience Optimization Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickExperienceOptimizationLink()
    {
        experienceOptimization.Click();
        return this;
    }

    /// <summary>
    ///     Click on Experience Profile Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickExperienceProfileLink()
    {
        experienceProfile.Click();
        return this;
    }

    /// <summary>
    ///     Click on Federated Experience Manager Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickFederatedExperienceManagerLink()
    {
        federatedExperienceManager.Click();
        return this;
    }

    /// <summary>
    ///     Click on Komfo Social Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickKomfoSocialLink()
    {
        komfoSocial.Click();
        return this;
    }

    /// <summary>
    ///     Click on Less Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickLess1Link()
    {
        less1.Click();
        return this;
    }

    /// <summary>
    ///     Click on Less Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickLess2Link()
    {
        less2.Click();
        return this;
    }

    /// <summary>
    ///     Click on List Manager Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickListManagerLink()
    {
        listManager.Click();
        return this;
    }

    /// <summary>
    ///     Click on Logout Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickLogoutLink()
    {
        logout.Click();
        return this;
    }

    /// <summary>
    ///     Click on Marketing Control Panel Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickMarketingControlPanelLink()
    {
        marketingControlPanel.Click();
        return this;
    }

    /// <summary>
    ///     Click on Media Library Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickMediaLibraryLink()
    {
        mediaLibrary.Click();
        return this;
    }

    /// <summary>
    ///     Click on Monthly Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickMonthly1Link()
    {
        monthly1.Click();
        return this;
    }

    /// <summary>
    ///     Click on Monthly Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickMonthly2Link()
    {
        monthly2.Click();
        return this;
    }

    /// <summary>
    ///     Click on More Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickMore1Link()
    {
        more1.Click();
        return this;
    }

    /// <summary>
    ///     Click on More Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickMore2Link()
    {
        more2.Click();
        return this;
    }

    /// <summary>
    ///     Click on Path Analyzer Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickPathAnalyzerLink()
    {
        pathAnalyzer.Click();
        return this;
    }

    /// <summary>
    ///     Click on Recycle Bin Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickRecycleBinLink()
    {
        recycleBin.Click();
        return this;
    }

    /// <summary>
    ///     Click on Role Manager Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickRoleManagerLink()
    {
        roleManager.Click();
        return this;
    }

    /// <summary>
    ///     Click on Security Editor Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickSecurityEditorLink()
    {
        securityEditor.Click();
        return this;
    }

    /// <summary>
    ///     Click on User Manager Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickUserManagerLink()
    {
        userManager.Click();
        return this;
    }

    /// <summary>
    ///     Click on Weekly Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickWeekly1Link()
    {
        weekly1.Click();
        return this;
    }

    /// <summary>
    ///     Click on Weekly Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickWeekly2Link()
    {
        weekly2.Click();
        return this;
    }

    /// <summary>
    ///     Click on Workbox Link.
    /// </summary>
    /// <returns>The Launchpad class instance.</returns>
    public Launchpad ClickWorkboxLink()
    {
        workbox.Click();
        return this;
    }
}